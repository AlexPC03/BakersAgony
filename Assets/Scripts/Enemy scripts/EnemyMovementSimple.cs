using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSimple : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;
    public bool inverted;
    public bool separate;


    [Header("Atributes")]
    public float range;
    public float speed;
    public float nearSpeed;
    public bool drift;


    [Header("Information")]
    public float actualDistance;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inverted)
        {
            CheckOrientationInverse();
        }
        else
        {
            CheckOrientation();
        }

        actualDistance = (player.transform.position - transform.position).magnitude;

        if(actualDistance<range)
        {
            if(!drift)
            {
                if (actualDistance > range * 0.25)
                {
                    rb.velocity = (player.transform.position - transform.position).normalized * speed*3f;
                }
                else
                {
                    rb.velocity = (player.transform.position - transform.position).normalized * nearSpeed*3f;
                }
            }
            else
            {
                if (actualDistance > range / 2)
                {
                    rb.AddForce((player.transform.position - transform.position).normalized * speed,ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce((player.transform.position - transform.position).normalized * nearSpeed,ForceMode2D.Force);
                }
            }

        }
        else if(actualDistance > range * 1.25)
        {
            rb.velocity = Vector2.zero;
        }


        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetBool("stopped", rb.velocity == Vector2.zero);
    }

    private void CheckOrientation()
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

    private void CheckOrientationInverse()
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
}
