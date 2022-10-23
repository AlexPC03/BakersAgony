using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimordialOvenController : BossController
{
    public AudioClip Open;
    public AudioClip Close;
    public AudioClip Die;

    private bool stop=false;
    private CoalHeartController heartLife;
    private GameObject player;
    private Animator anim;
    private float time=0;
    private float timepassed;
    public GameObject bigDoor;
    public GameObject[] smallDoors;
    public GameObject heart;
    public GameObject proyectile;
    public GameObject[] EnemyList;
    public float timeToStart;
    public bool enemies;
    public bool started;

    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        player = GameObject.FindGameObjectWithTag("Player");
        heartLife = heart.GetComponent<CoalHeartController>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        timepassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        anim.SetFloat("TimePassed", time);
        if(stop)
        {
            timepassed = 0;
        }
        else
        {
            timepassed += Time.deltaTime;
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length != 0;
        if(enemies)
        {
            anim.SetBool("OpenBig",false);
        }
        if (timepassed>timeToStart )
        {
            anim.SetTrigger("OpenSmall");
            stop = true;
        }
        else if(!enemies)
        {
            anim.SetBool("OpenBig",true);
            stop = false;
        }

        if(heart == null)
        {
            aud.pitch = 0.75f;
            aud.volume = 0.75f;
            aud.clip = Die;
            PlayOnce();
        }
    }

    public void Shoot()
    {
        if(heartLife!=null)
        {
            for(int i=0;i<14-(heartLife.vida / 50);i++)
            {
                GameObject proy;
                proy = Instantiate(proyectile, bigDoor.transform.position, new Quaternion(0, 0, 0, 0));
                if (proy.GetComponent<BreadMageProyectileMovement>() != null)
                {
                    proy.GetComponent<BreadMageProyectileMovement>().rotateVelocity = (15 - heartLife.vida / 50)*30;
                    proy.GetComponent<BreadMageProyectileMovement>().velocity = 16 - heartLife.vida / 50;
                    proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-18f, 18f), Random.Range(-18f, 18f), 0);
                }
            }
        }

    }
    public void Spawn()
    {
        foreach(GameObject door in smallDoors)
        {
            Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
            if(heartLife.vida<= heartLife.maxVida/2)
            {
                if(Random.Range(0f,1f)>0.5)
                {
                    Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
                }
            }
            else if (heartLife.vida <= heartLife.maxVida / 4)
            {
                if (Random.Range(0f, 1f) > 0.66)
                {
                    Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }

    public void openSmallSound()
    {
        if (heart != null)
        {
            aud.pitch = 0.75f;
            aud.clip = Open;
            aud.Play();
        }
    }

    public void closeSmallSound()
    {
        if (heart != null)
        {
            aud.pitch = 0.75f;
            aud.clip = Close;
            aud.Play();
        }
    }

    public void openBigSound()
    {
        if (heart != null)
        {
            aud.pitch = 2f;
            aud.clip = Open;
            aud.Play();
        }
    }

    public void closeBigSound()
    {
        if (heart != null)
        {
            aud.pitch = 2f;
            aud.clip = Close;
            aud.Play();
        }

    }

    private void PlayOnce()
    {
        if (!started)
        {
            aud.Play();
            started = true;
        }
    }

}
