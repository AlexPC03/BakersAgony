using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSimple : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;
    private float t=0;
    public bool inverted;
    public bool exploded = false;



    [Header("Atributes")]
    public float range;
    public float speed;
    public float nearSpeed;
    public bool randomSpeed=false;
    public bool drift;
    public bool explode=false;
    public float explodeDistance;


    [Header("Information")]
    public float actualDistance;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(randomSpeed)
        {
            speed = speed * Random.Range(0.5f, 1.5f);
            nearSpeed = nearSpeed * Random.Range(0.5f, 1.5f);
            rb.drag= rb.drag * Random.Range(0.5f, 1.1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inverted)
        {
            CheckOrientationInverse();
        }
        else
        {
            CheckOrientation();
        }

        actualDistance = (player.transform.position+new Vector3(0,-0.3f,0) - transform.position).magnitude;

        if(actualDistance<range)
        {
            if(!drift)
            {
                if (actualDistance > range * 0.25)
                {
                    rb.velocity = (player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * speed*3f;
                }
                else
                {
                    rb.velocity = (player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * nearSpeed*3f;
                }
            }
            else
            {
                if (actualDistance > range / 2)
                {
                    rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * speed,ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * nearSpeed,ForceMode2D.Force);
                }
            }
            if(explode && actualDistance<explodeDistance)
            {
                t += Time.deltaTime * 2;
                vida = 0;
                transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.25f, 1.25f, 1),t);
                transform.position = Vector3.MoveTowards(transform.position,player.transform.position + new Vector3(0, -0.3f, 0), t/30 * nearSpeed);
                if (!exploded)
                {
                    TakeDamage(1);
                    exploded = true;
                }
            }

        }
        else if(actualDistance > range * 1.25)
        {
            rb.velocity = Vector2.zero;
        }

        if(anim!=null)
        {
            anim.SetFloat("Yvelocity", rb.velocity.y);
            anim.SetBool("stopped", rb.velocity == Vector2.zero);
        }

    }

    private void CheckOrientation()
    {
        if(sp!=null)
        {
            if(rb.velocity.x<-0.01)
            {
                sp.flipX = false;
            }
            else if (rb.velocity.x > 0.01)
            {
                sp.flipX = true;
            }
        }
        else
        {
            if (rb.velocity.x < -0.01)
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else if (rb.velocity.x > 0.01)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    private void CheckOrientationInverse()
    {
        if (sp != null)
        {
            if (rb.velocity.x > -0.01)
            {
                sp.flipX = false;
            }
            else if (rb.velocity.x < 0.01)
            {
                sp.flipX = true;
            }
        }
        else
        {
            if (rb.velocity.x > -0.01)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (rb.velocity.x < 0.01)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
