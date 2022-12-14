using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimordialOvenController : BossController
{
    private Camera camara;
    public AudioClip Open;
    public AudioClip Close;
    public AudioClip Die;

    private bool stop=false;
    private int smallTimes=0;
    private CoalHeartController heartLife;
    private GameObject player;
    public Animator anim;
    private float time=0;
    private float timepassed;
    public GameObject bigDoor;
    public GameObject[] smallDoors;
    public GameObject heart;
    public GameObject proyectile;
    public float poyectileVariation;
    public GameObject[] EnemyList;
    public float timeToStart;
    public bool enemies;
    public bool started;

    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        camara=Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        heartLife = heart.GetComponent<CoalHeartController>();
        aud = GetComponent<AudioSource>();

        timepassed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (timepassed>timeToStart && smallTimes<=4)
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
                proy = Instantiate(proyectile, new Vector3(bigDoor.transform.position.x+Random.Range(-0.1f,0.1f), bigDoor.transform.position.y - 1, 0f), new Quaternion(0, 0, 0, 0));
                if (proy.GetComponent<BreadMageProyectileMovement>() != null)
                {
                    //proy.GetComponent<BreadMageProyectileMovement>().ghost = true;
                    proy.GetComponent<BreadMageProyectileMovement>().rotateVelocity = (15 - heartLife.vida / (heartLife.maxVida/10)) *30;
                    proy.GetComponent<BreadMageProyectileMovement>().velocity = 16 - heartLife.vida / (heartLife.maxVida / 10);
                    proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-poyectileVariation, poyectileVariation), Random.Range(-poyectileVariation, poyectileVariation), 0);
                }
                else if (proy.GetComponent<BumeranProyectileMovement>() != null)
                {
                    //proy.GetComponent<BumeranProyectileMovement>().ghost = true;
                    proy.GetComponent<BumeranProyectileMovement>().catchDistance = 4;
                    proy.GetComponent<BumeranProyectileMovement>().rotateVelocity = (14 - heartLife.vida / (heartLife.maxVida / 10)) * 30;
                    proy.GetComponent<BumeranProyectileMovement>().velocity = 13 - heartLife.vida / (heartLife.maxVida / 10);
                    proy.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-poyectileVariation, poyectileVariation), Random.Range(-poyectileVariation, poyectileVariation), 0);
                }
            }
        }

    }
    public void Spawn()
    {
        foreach(GameObject door in smallDoors)
        {
            GameObject obj= Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
            if(obj.GetComponent<BreadMageProyectileMovement>()!=null)
            {
                obj.GetComponent<BreadMageProyectileMovement>().targetPos=player.transform.position+ new Vector3(Random.Range(-poyectileVariation, poyectileVariation), Random.Range(-poyectileVariation, poyectileVariation), 0);
                obj.GetComponent<BreadMageProyectileMovement>().target = player;
            }
            if (heartLife.vida<= heartLife.maxVida/2)
            {
                if(Random.Range(0f,1f)>0.5)
                {
                    GameObject obj2=Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
                    if (obj2.GetComponent<BreadMageProyectileMovement>() != null)
                    {
                        obj2.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-poyectileVariation, poyectileVariation), Random.Range(-poyectileVariation, poyectileVariation), 0);
                        obj2.GetComponent<BreadMageProyectileMovement>().target = player;
                    }
                }
            }
            else if (heartLife.vida <= heartLife.maxVida / 4)
            {
                if (Random.Range(0f, 1f) > 0.66)
                {
                    GameObject obj3=Instantiate(EnemyList[Random.Range(0, EnemyList.Length)], new Vector3(door.transform.position.x, door.transform.position.y - 2, -0.5f), new Quaternion(0, 0, 0, 0));
                    if (obj3.GetComponent<BreadMageProyectileMovement>() != null)
                    {
                        obj3.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-poyectileVariation, poyectileVariation), Random.Range(-poyectileVariation, poyectileVariation), 0);
                        obj3.GetComponent<BreadMageProyectileMovement>().target = player;
                    }
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
            camara.GetComponent<Animator>().SetTrigger("BigShake");
            aud.Play();
            started = true;
        }
    }

    public void increaseSmallTimes()
    {
        smallTimes++;
    }

    public void resetSmallTimes()
    {
        smallTimes=0;
    }

}
