using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoscoController : BossController
{
    private bool stopped=false;
    private float time=0;
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
    void Update()
    {
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
        if(SpawnHit.Length>0)
        {
            foreach (var obj in SpawnHit)
            {
                GameObject Inst=Instantiate(obj,transform.position,new Quaternion(0,0,0,0));
                if(Inst.GetComponent<BreadMageProyectileMovement>()!=null)
                {
                    Inst.GetComponent<BreadMageProyectileMovement>().targetPos=player.transform.position;
                    Inst.GetComponent<BreadMageProyectileMovement>().target=player;
                }
                if (Inst.GetComponent<BumeranProyectileMovement>() != null)
                {
                    Inst.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position;
                    Inst.GetComponent<BumeranProyectileMovement>().target = player;
                }
            }
        }

    }

    public void SpawnOnAttack()
    {
        if (SpawnAttack.Length > 0)
        {
            foreach (var obj in SpawnAttack)
            {
                GameObject Inst = Instantiate(obj, transform.position, new Quaternion(0, 0, 0, 0));
                if (Inst.GetComponent<BreadMageProyectileMovement>() != null)
                {
                    Inst.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position;
                    Inst.GetComponent<BreadMageProyectileMovement>().target = player;
                }
                if (Inst.GetComponent<BumeranProyectileMovement>() != null)
                {
                    Inst.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position;
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
        wind.SetActive(true);
    }

    public void StopWind()
    {
        wind.SetActive(false);
    }

    public void StopSpeed()
    {
        stopped = true;
        rb.velocity =Vector2.zero;
    }

    public void RestartSpeed()
    {
        stopped = false;
        rb.drag = 0;
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
