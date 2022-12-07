using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeHaundController : EnemyBasicLifeSystem
{
    private GameObject player;
    private Vector3 add;
    private Rigidbody2D rb;
    private Animator anim;
    [Header("Atributes")]
    public float range;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        add = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((player.transform.position - transform.position).magnitude<range)
        {
            anim.SetTrigger("Start");
        }
        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetFloat("Vida", vida);
        CheckOrientation();
    }

    public void Impulse()
    {
        rb.velocity = Vector2.zero;
        if(((player.transform.position + add) - transform.position).magnitude>1.5)
        {
            rb.AddForce(((player.transform.position+add) - transform.position).normalized * speed,ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce((player.transform.position- transform.position).normalized * speed, ForceMode2D.Impulse);
            add = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        }
        anim.speed = Random.Range(0.9f, 1.1f);
        GetComponent<AudioSource>().pitch = Random.Range(0.4f, 0.6f);
        GetComponent<AudioSource>().Play();
        GetComponent<ParticleSystem>().Play();
    }

    private void CheckOrientation()
    {
        if (sp != null)
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
        else
        {
            if (rb.velocity.x < -0.01)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (rb.velocity.x > 0.01)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
