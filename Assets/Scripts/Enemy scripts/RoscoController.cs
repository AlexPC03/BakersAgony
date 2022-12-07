using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoscoController : BossController
{
    private float hitTime = 0;
    private bool stopped=false;
    private float time=10;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject wind;
    public GameObject[] SpawnHit;
    public GameObject[] SpawnAttack;
    public float speed;
    public float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        RestartSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitTime += Time.deltaTime;
        time += Time.deltaTime;
        rb.velocity = rb.velocity.normalized * speed;
        if(time>attackTime)
        {
            anim.SetTrigger("Stand");
            time = 0;
        }
        if(stopped)
        {
            rb.velocity = Vector2.zero;
        }

    }

    public void SpawnOnHit()
    {
        if(time > 1 && hitTime>0.5)
        {
            if(SpawnHit.Length>0)
            {
                foreach (var obj in SpawnHit)
                {
                    GameObject Inst=Instantiate(obj,transform.position,new Quaternion(0,0,0,0));
                    if(Inst.GetComponent<BreadMageProyectileMovement>()!=null)
                    {
                        Inst.GetComponent<BreadMageProyectileMovement>().targetPos=player.transform.position+new Vector3(Random.Range(-2f,2f), Random.Range(-2f, 2f),0);
                        Inst.GetComponent<BreadMageProyectileMovement>().target=player;
                    }
                    if (Inst.GetComponent<BumeranProyectileMovement>() != null)
                    {
                        Inst.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
                        Inst.GetComponent<BumeranProyectileMovement>().target = player;
                    }
                }
                hitTime = 0;
            }
        }
    }

    public void SpawnOnAttack()
    {
        if (SpawnAttack.Length > 0)
        {
            foreach (var obj in SpawnAttack)
            {
                GameObject Inst = Instantiate(obj, transform.position + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), new Quaternion(0, 0, 0, 0));
                if (Inst.GetComponent<BreadMageProyectileMovement>() != null)
                {
                    Inst.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                    Inst.GetComponent<BreadMageProyectileMovement>().target = player;
                }
                if (Inst.GetComponent<BumeranProyectileMovement>() != null)
                {
                    Inst.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                    Inst.GetComponent<BumeranProyectileMovement>().target = player;
                }
            }
        }
    }

    public void DownCollision()
    {
        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        SpawnOnHit();
    }

    public void SideCollision()
    {
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        SpawnOnHit();
    }
    
    public void StopAttack()
    {
        time=0;
    }

    public void StartWind()
    {
        wind.SendMessage("Appear");
    }

    public void StopWind()
    {
        wind.SendMessage("Disappear");
    }

    public void StopSpeed()
    {
        stopped = true;
        rb.velocity =Vector2.zero;
    }

    public void RestartSpeed()
    {
        stopped = false;
        if (Random.Range(0, 2) == 0)
        {
            rb.velocity = new Vector2(1, -1) * speed;
        }
        else
        {
            rb.velocity = new Vector2(-1, -1) * speed;
        }
    }
}
