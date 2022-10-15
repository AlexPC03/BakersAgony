using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;
    private SpriteRenderer sp;
    private Animator anim;

    [Header("Atributes")]
    public float range;
    public float speed;
    public float chargeSpeed;
    public float chargeRecover;
    public chargeDirection directionFromCharge;
    public enum chargeDirection
    {
        Vertical,
        Horizontal,
        Both
    }

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
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        charging = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOrientation();
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
        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetBool("Stopped", rb.velocity == Vector2.zero);
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

            if (xDistance < 0.01 && xDistance > -0.01 )
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                if(directionFromCharge == chargeDirection.Vertical || directionFromCharge == chargeDirection.Both)
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

            }
            if (yDistance < 0.01 && yDistance > -0.01)
            {
                rb.velocity = new Vector2(rb.velocity.x,0);
                if (directionFromCharge == chargeDirection.Horizontal || directionFromCharge == chargeDirection.Both)
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
                else
                {
                    if (xDistance < 0)
                    {
                        rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
                    }
                }

            }
        }
    }

    private void Charge(Vector2 direction)
    {
        anim.SetTrigger("Charge");
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * chargeSpeed, ForceMode2D.Impulse);
        Invoke("StopCharging", chargeRecover);
        Invoke("CheckOrientation", 0.1f);
    }

    private void StopCharging()
    {
        rb.velocity = Vector2.zero;
        charging = false;
    }

    private void CheckOrientation()
    {
        if (rb.velocity.x < -0.01)
        {
            sp.flipX = false;
        }
        else if (rb.velocity.x > 0.01)
        {
            sp.flipX = true;
        }
    }
}
