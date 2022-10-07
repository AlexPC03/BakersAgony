using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSimple : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
