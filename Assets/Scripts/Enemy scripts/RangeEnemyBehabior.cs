using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehabior : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;
    private GameObject proy;

    [Header("Atributes")]
    public Vector3 shootPosition;
    private Vector3 actualshootPosition;
    public GameObject proyectile;
    public int shootCuantity=1;
    public float spread=0;
    public float nextShootTime;
    public float range;
    public float minShootRange;
    public float maxShootRange;
    public float speed;
    public bool wait;
    public bool recoil;
    public float recoilForce;
    public bool changeOnAttack = false;
    private float actualVida;
    public bool changeOnShoot = false;
    private bool melee = false;

    [Header("Information")]
    public float actualDistance;
    public bool canShoot;
    public float shootTimer;
    public bool firstShoot;



    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canShoot = true;
        firstShoot = false;
        if(changeOnAttack)
        {
            actualVida = vida;
            if(Random.Range(0,2)==0)
            {
                melee = true;
            }
        }
        actualshootPosition = shootPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckOrientation();

        if (wait && firstShoot)
        {
            if (proy != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
        actualDistance = (player.transform.position - transform.position).magnitude;

        if (shootTimer>nextShootTime && !canShoot)
        {
            canShoot = true;
        }

        if(actualDistance<range)
        {
            if(!melee)
            {
                if(actualDistance < maxShootRange && actualDistance > minShootRange && canShoot)
                {
                    canShoot = false;
                    rb.velocity = Vector2.zero;
                    anim.SetTrigger("Shoot");
                }
                else if (actualDistance > maxShootRange)
                {
                    rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * speed, ForceMode2D.Force);
                }
                else if(actualDistance < minShootRange)
                {
                    rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * -speed, ForceMode2D.Force);
                }
            }
            else
            {
                rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).normalized * speed, ForceMode2D.Force);
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

        if(changeOnAttack && vida != actualVida)
        {
            melee = !melee;
            actualVida = vida;
        }
        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetBool("Stopped", rb.velocity == Vector2.zero);
    }

    public void Shoot()
    {
        firstShoot = true;
        shootTimer = 0;
        for(int i=0;i<shootCuantity; i++)
        {
            proy = Instantiate(proyectile, transform.position + actualshootPosition, new Quaternion(0, 0, 0, 0));
            if (proy.GetComponent<BreadMageProyectileMovement>()!=null)
            {
                proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position+new Vector3(Random.Range(-spread/10, spread/10), Random.Range(-spread / 10, spread / 10),0);
            }
            else if(proy.GetComponent<BumeranProyectileMovement>() != null)
            {
                proy.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-spread / 10, spread / 10), Random.Range(-spread / 10, spread / 10), 0);
            }
        }
        if(recoil)
        {
            rb.AddForce((transform.position - player.transform.position).normalized*recoilForce,ForceMode2D.Impulse);
        }
        if(changeOnShoot)
        {
            melee = true;
        }
        if(GetComponent<ParticleSystem>()!=null)
        {
            if (!GetComponent<ParticleSystem>().main.loop)
            {
                GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void CheckOrientation()
    {
        if (rb.velocity.x < -0.01)
        {
            sp.flipX = false;
            actualshootPosition = shootPosition;

        }
        else if (rb.velocity.x > 0.01)
        {
            sp.flipX = true;
            actualshootPosition = new Vector3(-shootPosition.x, shootPosition.y, shootPosition.z);
        }
    }
}
