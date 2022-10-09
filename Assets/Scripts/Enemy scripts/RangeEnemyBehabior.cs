using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehabior : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;
    private SpriteRenderer sp;
    private Animator anim;

    [Header("Atributes")]
    public Vector3 shootPosition;
    public GameObject proyectile;
    public float nextShootTime;
    public float range;
    public float minShootRange;
    public float maxShootRange;
    public float speed;

    [Header("Information")]
    public float actualDistance;
    public bool canShoot;
    public float shootTimer;



    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOrientation();

        actualDistance = (player.transform.position - transform.position).magnitude;

        if (shootTimer>nextShootTime && !canShoot)
        {
            canShoot = true;
        }

        if(actualDistance<range)
        {
            if(actualDistance < maxShootRange && actualDistance > minShootRange && canShoot)
            {
                canShoot = false;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Shoot");
            }
            else if (actualDistance > maxShootRange)
            {
                rb.AddForce((player.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
            }
            else if(actualDistance < minShootRange)
            {
                rb.AddForce((player.transform.position - transform.position).normalized * -speed, ForceMode2D.Force);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(shootTimer<nextShootTime)
        {
            shootTimer += Time.deltaTime;
        }


        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetBool("Stopped", rb.velocity == Vector2.zero);
    }

    public void Shoot()
    {
        shootTimer = 0;
        GameObject proy= Instantiate(proyectile, transform.position+shootPosition,new Quaternion(0,0,0,0));
        proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position;
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
