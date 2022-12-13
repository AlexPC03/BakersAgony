using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguettePartController : BossController
{
    private bool med = true;
    private bool mult = false;
    public AudioSource audShake;
    private Camera camara;
    public ParticleSystem part;
    public float distanceToStop=30;
    public int side;
    private GameObject player;
    private bool damaged=false;
    public GameObject parent;
    public GameObject son;
    public float chargingSpeed;
    public float plusMaxSpeed=1;
    public float aimingTime;
    public bool aiming=true;
    public bool attacking=false;
    public GameObject[] spawn;
    [Range(0, 1)]
    public float spawnProbability;
    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        maxVida += 300;
        vida += 300;
        camara = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        if(transform.parent!=null && transform.parent.gameObject.GetComponent<BaguettePartController>()!=null)
        {
            parent = gameObject.transform.parent.gameObject;
        }
        if (transform.childCount > 0)
        {
            if(transform.GetChild(0).gameObject.GetComponent<BaguettePartController>() != null)
            {
                son = transform.GetChild(0).gameObject;
            }
        }
        if (Random.Range(0, 2) == 0)
        {
            side = 1;
        }
        else
        {
            side = -1;
        }
        if(!mult)
        {
            chargingSpeed *= 3;
            mult = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (vida > 0)
        {
            if (aiming)
            {
                Aim();
            }
            if (attacking)
            {
                Attack();
            }
        }
        else
        {
            if (parent != null)
            {
                parent.GetComponent<BaguettePartController>().vida = 0;
            }
        }
        EqualLife();
        if(vida<=maxVida/2 && med)
        {
            chargingSpeed = chargingSpeed * plusMaxSpeed;
            med = false;
        }

    }

    private void Aim()
    {
        if (parent == null && aiming && vida > 0)
        {
            if(side==-1)
            {
                if (Random.Range(0,3)==0)
                {
                    side = -1;
                }
                else
                {
                    side = 1;
                }
            }
            else
            {
                if (Random.Range(0, 3) == 0)
                {
                    side = 1;
                }
                else
                {
                    side = -1;
                }
            }

            float y = player.transform.position.y + Random.Range(-1f, 1f);
            transform.position = new Vector3((player.transform.position.x + distanceToStop+20) * side, y, (y - 0.75f) / 10);
            if(transform.position.x<player.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
            aiming = false;
            Invoke("ReadyForAttack",aimingTime*vida/maxVida+Random.Range(-0.5f,1f));
        }
    }

    private void Attack()
    {
        if (parent == null && vida > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3((player.transform.position.x + distanceToStop+41) * -side,transform.position.y, (transform.position.y-0.75f) / 10),chargingSpeed/ 20);
            if(Mathf.Abs(player.transform.position.x-transform.position.x) >= distanceToStop+30)
            {
                aiming = true;
                attacking = false;
            }
        }
    }

    public void ReadyForAttack()
    {
        attacking = true;
    }
    
    public void EqualLife()
    {
        if(parent!=null)
        {
            if(parent.GetComponent<BaguettePartController>().vida > vida)
            {
                parent.GetComponent<BaguettePartController>().vida = vida;
            }
            if(vida<=0 && !damaged)
            {
                parent.GetComponent<BaguettePartController>().vida = 0;
                parent.SendMessage("TakeDamage", 1);
                damaged = true;
            }
        }
        if(son!=null)
        {
            if (son.GetComponent<BaguettePartController>().vida > vida)
            {
                son.GetComponent<BaguettePartController>().vida = vida;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.isTrigger &&collision.gameObject.layer==6 && part!=null )
        {
            if(!part.isPlaying)
            {
                part.Play();
                camara.GetComponent<Animator>().SetTrigger("Shake");
                if(audShake!=null && attacking)
                {
                    audShake.pitch = Random.Range(0.8f, 1f);
                    audShake.Play();
                }
            }
        }
        if (collision.tag == "Player" && gameObject.layer != 6)
        {
            collision.SendMessage("TakeDamage");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.layer == 6 && part != null )
        {
            if (!part.isPlaying)
            {
                part.Play();
                camara.GetComponent<Animator>().SetTrigger("Shake");
                if (audShake != null && attacking)
                {
                    audShake.pitch = Random.Range(0.8f, 1f);
                    audShake.Play();

                }
            }
            if (spawn.Length > 0)
            {
                foreach (GameObject ins in spawn)
                {
                    if (Random.Range(0, 1f) < spawnProbability)
                    {
                        GameObject obj = Instantiate(ins, transform.position + new Vector3(0, Random.Range(-0.1f, 0.1f), 0), new Quaternion(0, 0, 0, 0));
                        if (obj.GetComponent<BreadMageProyectileMovement>())
                        {
                            obj.GetComponent<BreadMageProyectileMovement>().target = player;
                            obj.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
                        }
                        else
                        {
                            obj.tag = "SpecialEnemy";
                        }
                    }

                }
            }
        }
    }
}
