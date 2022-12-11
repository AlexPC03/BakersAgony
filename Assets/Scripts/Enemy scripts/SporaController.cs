using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporaController : BossController
{
    private float timeSpit;
    private float timeIdle;
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject player;
    private bool idle=true;
    private bool spiting=false;
    public GameObject face;
    public float idleTime=0;


    private float trueSlamProb;
    [Header("Slam")]
    public float slamProb;
    public GameObject slamSpawn;
    public int slamSpawnNumber;
    public float slamSpawnRange;
    public float slamSpread = 0.5f;


    private float trueSmileProb;
    [Header("Smile")]
    public float smileProb;
    public GameObject smileSpawn;
    public int smileSpawnNumber;
    public float smileSpawnRange;


    [Header("Spit")]
    public float spitProb;
    public float spitRate;
    public GameObject spitSpawn;
    public float spitSpread = 0.5f;
    public float SpitRecoilSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        trueSlamProb = slamProb / (slamProb + smileProb + spitProb);
        trueSmileProb = smileProb / (slamProb + smileProb + spitProb);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spiting)
        {
            timeSpit += Time.deltaTime;
            if (timeSpit>spitRate)
            {
                Shoot(spitSpawn);
                timeSpit = 0;
            }
        }
        else if(idle)
        {
            if (timeIdle>=idleTime)
            {
                float i = Random.Range(0f, 1f);
                if(i< trueSlamProb)
                {
                    Slam();
                    trueSlamProb -= 0.25f;
                }
                else if (i >= trueSlamProb && i <= trueSlamProb+trueSmileProb)
                {
                    Smile();
                }
                else if(i > trueSlamProb+trueSmileProb)
                {
                    Spit();
                }
                timeIdle = 0;
            }
            else
            {
                timeIdle += Time.deltaTime;
            }
        }
    }

    public void StartIdle()
    {
        idle = true;
        spiting = false;
        timeSpit = 0;
    }

    public void Slam()
    {
        anim.SetTrigger("Slam");
        idle = false;
    }

    public void Smile()
    {
        idle = false;
        anim.SetTrigger("Smile");
    }

    public void Spit()
    {
        idle = false;
        anim.SetTrigger("Spit");
    }

    public void SlamSpawn()
    {
        float n;
        if(vida<=maxVida/2)
        {
            n = slamSpawnNumber * 2;
        }
        else
        {
            n = slamSpawnNumber;
        }
        for(int i =0;i<n;i++)
        {
            GameObject obj;
            obj = Instantiate(slamSpawn, player.transform.position + new Vector3(Random.Range(-slamSpawnRange, slamSpawnRange), Random.Range(-slamSpawnRange, slamSpawnRange), 0), new Quaternion(0, 0, 0, 0));

            if (obj.GetComponent<BreadMageProyectileMovement>() != null)
            {
                obj.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-slamSpread / 10, slamSpread / 10), Random.Range(-slamSpread / 10, slamSpread / 10), 0);
            }
            else if (obj.GetComponent<BumeranProyectileMovement>() != null)
            {
                obj.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-slamSpread / 10, slamSpread / 10), Random.Range(-slamSpread / 10, slamSpread / 10), 0);
            }
        }
    }

    public void SmileSpawn()
    {
        float n;
        if (vida <= maxVida / 2)
        {
            n = smileSpawnNumber * 1.5f;
        }
        else
        {
            n = smileSpawnNumber;
        }
        for (int i = 0; i < n; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-smileSpawnRange, smileSpawnRange), Random.Range(-smileSpawnRange, smileSpawnRange), 0);
            GameObject obj = Instantiate(smileSpawn, transform.position + pos, new Quaternion(0, 0, 0, 0));
            if (obj.GetComponent<BreadMageProyectileMovement>() != null)
            {
                obj.GetComponent<BreadMageProyectileMovement>().targetPos = pos+transform.position+ new Vector3(Random.Range(-smileSpawnRange, smileSpawnRange), Random.Range(-smileSpawnRange, smileSpawnRange));
            }
            else if (obj.GetComponent<BumeranProyectileMovement>() != null)
            {
                obj.GetComponent<BumeranProyectileMovement>().targetPos = (pos + transform.position+new Vector3(Random.Range(-smileSpawnRange, smileSpawnRange), Random.Range(-smileSpawnRange, smileSpawnRange))) * Random.Range(smileSpawnRange, smileSpawnRange*10);
            }
        }
    }

    private void Shoot(GameObject proyectile)
    {
        float n;
        if (vida <= maxVida / 2)
        {
            n = SpitRecoilSpeed * 1.5f;
        }
        else
        {
            n = SpitRecoilSpeed;
        }
        GameObject proy = Instantiate(proyectile, transform.position, new Quaternion(0, 0, 0, 0));
        if (proy.GetComponent<BreadMageProyectileMovement>() != null)
        {
            proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-spitSpread / 10, spitSpread / 10), Random.Range(-spitSpread / 10, spitSpread / 10), 0);
            rb.AddForce((transform.position - player.transform.position).normalized * n, ForceMode2D.Force);
        }
        else if (proy.GetComponent<BumeranProyectileMovement>() != null)
        {
            proy.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-spitSpread / 10, spitSpread / 10), Random.Range(-spitSpread / 10, spitSpread / 10), 0);
            rb.AddForce((transform.position - player.transform.position).normalized * n, ForceMode2D.Force);
        }
    }

    public void Shake()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger == false && (collision.gameObject.layer == 6 || collision.gameObject.tag == "Boss"))
        {
            rb.AddForce((player.transform.position - transform.position).normalized * SpitRecoilSpeed / 10, ForceMode2D.Impulse);
        }
    }

    public void StartSpit()
    {
        spiting = true;
        idle = false;
    }
}
