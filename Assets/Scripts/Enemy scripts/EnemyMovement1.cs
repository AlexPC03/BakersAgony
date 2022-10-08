using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;

    [Header("Atributes")]
    public float range;
    public float speed;
    public float chargeSpeed;
    public float chargeRecover;

    [Header("Information")]
    public float  actualDistance;
    public float xDistance, yDistance;
    public bool charging;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        charging = false;
    }

    // Update is called once per frame
    void Update()
    {
        actualDistance = (player.transform.position - transform.position).magnitude;
        xDistance = player.transform.position.x - transform.position.x;
        yDistance = player.transform.position.y - transform.position.y;

        if (actualDistance < range)
        {
            Align();
        }
        else if (actualDistance > range * 1.25)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Align()
    {
        if(!charging)
        {
            if(Mathf.Abs(xDistance) < Mathf.Abs(yDistance))
            {
                if(xDistance<0)
                {
                    rb.AddForce(Vector2.left*speed, ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(Vector2.right*speed, ForceMode2D.Force);
                }
            }
            else
            {
                if (yDistance < 0)
                {
                    rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(Vector2.up * speed, ForceMode2D.Force);
                }
            }

            if (xDistance < 0.05 && xDistance > -0.05)
            {
                charging = true;
                if (yDistance < 0)
                {
                    Charge(Vector2.down);
                }
                else
                {
                    Charge(Vector2.up);
                }
            }
            if (yDistance < 0.05 && yDistance > -0.05)
            {
                charging = true;
                if (xDistance < 0)
                {
                    Charge(Vector2.left);
                }
                else
                {
                    Charge(Vector2.right);
                }
            }
        }
    }

    private void Charge(Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * chargeSpeed, ForceMode2D.Impulse);
        Invoke("StopCharging", chargeRecover);
    }

    private void StopCharging()
    {
        rb.velocity = Vector2.zero;
        charging = false;
    }

    //Intento de deteción

    //private bool InSight(Transform target)
    //{
    //    RaycastHit hit;
    //    if (Physics.Linecast(transform.position, target.transform.position, out hit))
    //    {
    //        if (hit.transform.tag == "Player")
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
