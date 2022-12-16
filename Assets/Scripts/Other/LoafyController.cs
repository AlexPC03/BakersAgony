using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LoafyController : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private playerMovement playerController;
    private GameObject target;
    private int actualroom = 0;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;

        if (GetComponent<SpriteRenderer>().enabled == true)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length>=0)
            {
                target = GetClosestEnemy(enemies);
            }

            if(target!=null)
            {
                Move();
            }

            if (playerController.sala != actualroom)
            {
                aud.pitch = Random.Range(1, 1.5f);
                aud.Play();
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Dig");
                actualroom = playerController.sala;
            }
            if(gamepad==null)
            {
                if(Input.GetKeyDown(KeyCode.E)||Input.GetMouseButtonDown(0))
                {
                    aud.pitch = Random.Range(1, 1.5f);
                    aud.Play();
                    rb.velocity = Vector2.zero;
                    anim.SetTrigger("Dig");
                    actualroom = playerController.sala;
                }
            }
            else
            {
                if(gamepad.aButton.wasPressedThisFrame || gamepad.bButton.wasPressedThisFrame || gamepad.crossButton.wasPressedThisFrame || gamepad.rightTrigger.wasPressedThisFrame)
                {
                    aud.pitch = Random.Range(1, 1.5f);
                    aud.Play();
                    rb.velocity = Vector2.zero;
                    anim.SetTrigger("Dig");
                    actualroom = playerController.sala;
                }
            }
            anim.SetFloat("Xvelocity",Mathf.Abs(rb.velocity.x));
            anim.SetFloat("Yvelocity", rb.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag == "SpecialEnemy")
        {
            if (collision.GetComponent<BreadEnemy>() == null)
            {
                collision.SendMessage("TakeDamage", 10 * playerController.attack * playerController.attackMultiplier);
            }
            else
            {
                collision.SendMessage("TakeDamage", 5 * playerController.attack * playerController.attackMultiplier);
            }
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * playerController.attack * playerController.attackMultiplier*0.25f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag == "SpecialEnemy")
        {
            if (collision.GetComponent<BreadEnemy>() == null)
            {
                collision.SendMessage("TakeDamage", 10 * playerController.attack * playerController.attackMultiplier);
            }
            else
            {
                collision.SendMessage("TakeDamage", 5 * playerController.attack * playerController.attackMultiplier);
            }
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * playerController.attack * playerController.attackMultiplier * 0.25f, ForceMode2D.Impulse);
        }
    }

    public void Return()
    {
        transform.position=player.transform.position+new Vector3(Random.Range(0.1f,0.3f), Random.Range(0.1f, 0.3f),0);
    }

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    private void Move()
    {
        float xDistance = Mathf.Abs(target.transform.position.x - transform.position.x);
        float YDistance = Mathf.Abs(target.transform.position.y - transform.position.y);
        if(xDistance<YDistance)
        {
            if(target.transform.position.y - transform.position.y<0)
            {
                rb.velocity = new Vector2(0, (float)(-playerController.runSpeed * playerController.speedMultiplier * 0.75));
            }
            else
            {
                rb.velocity = new Vector2(0, (float)(playerController.runSpeed * playerController.speedMultiplier * 0.75));
            }
        }
        else
        {
            if (target.transform.position.x - transform.position.x < 0)
            {
                rb.velocity = new Vector2((float)(-playerController.runSpeed * playerController.speedMultiplier * 0.75), 0);
            }
            else
            {
                rb.velocity = new Vector2((float)(playerController.runSpeed * playerController.speedMultiplier * 0.75), 0);
            }
        }
    }

}
