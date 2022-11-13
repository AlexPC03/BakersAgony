using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource aud;
    private Animator anim;
    private GameObject player;
    private playerMovement playerController;
    private Rigidbody2D rb;
    private GameObject target;
    private int actualroom=0;
    private float time = 10;
    private float timeR;
    // Start is called before the first frame update
    void Start()
    {
        timeR = Random.Range(2f, 9f);
        target = GameObject.Find("RatTarget");
        aud = GetComponent<AudioSource>();
        anim=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = player.transform.position+new Vector3(0,1,0);
        actualroom = playerController.sala;
        aud.clip = clips[Random.Range(0, clips.Length)];
        aud.pitch = Random.Range(0.9f, 1.1f);
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(target.GetComponent<SpriteRenderer>().enabled != true)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<AudioSource>().enabled = true;
            if (target!=null && rb != null  )
            {
                rb.velocity = (target.transform.position - transform.position).normalized * playerController.runSpeed * playerController.speedMultiplier * 0.75f;
            }
        }


        if(playerController.sala!=actualroom && target.GetComponent<SpriteRenderer>().enabled==true)
        {
            transform.position = player.transform.position + new Vector3(0.5f, 1, 0);
            actualroom=playerController.sala;
            aud.clip = clips[Random.Range(0, clips.Length)];
            aud.pitch = Random.Range(0.9f, 1.1f);
            aud.Play();
        }
        if(time> timeR && GetComponent<AudioSource>().enabled == true)
        {
            aud.clip = clips[Random.Range(0, clips.Length)];
            aud.pitch = Random.Range(0.9f, 1.1f);
            aud.Play();
            timeR = Random.Range(2f, 9f);
            time = 0;
        }
        checkOrientation();
        anim.SetFloat("velocity", rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target.GetComponent<SpriteRenderer>().enabled == true)
        {
            if(collision.tag=="Enemy" || collision.tag == "Boss" || collision.tag == "SpecialEnemy")
            {
                if(collision.GetComponent<BreadEnemy>()!=null)
                {
                    collision.SendMessage("TakeDamage", 10 * playerController.attack * playerController.attackMultiplier);
                }
                else
                {
                    collision.SendMessage("TakeDamage", 5 * playerController.attack * playerController.attackMultiplier);
                }
                if (!aud.isPlaying)
                {
                    aud.clip = clips[Random.Range(0, clips.Length)];
                    aud.pitch = Random.Range(0.9f, 1.1f);
                    aud.Play();
                }
                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * playerController.attack * playerController.attackMultiplier, ForceMode2D.Impulse);
            }
        }


    }

    private void checkOrientation()
    {
        if(rb.velocity.x>=0.01)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if(rb.velocity.x <= -0.01)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
