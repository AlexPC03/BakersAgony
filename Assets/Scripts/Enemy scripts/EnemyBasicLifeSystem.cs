using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicLifeSystem : MonoBehaviour
{
    private playerMovement playerController;
    private Color originalColor;
    public float pitch=1;
    protected AudioSource aud;
    private float timeToDamage = 0;
    private float invulneravility = 0.25f;
    protected SpriteRenderer sp;
    public GameObject corn;
    public GameObject corncub;
    [Range(0, 1)]
    public float cornWeight;
    public GameObject head;
    [Header("LifeParameters")]
    public float maxVida;
    public float deathTime;
    public float vida;

    // Start is called before the first frame update
    public void StartVida()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        maxVida = maxVida + playerController.sala;
        vida = maxVida;
        sp = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        if(sp!=null)
        {
            originalColor = sp.color;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (vida<=0)
        {
            if(GetComponent<Rigidbody2D>()!=null)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }

            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            if(GetComponent<ParticleSystem>()!=null)
            {
                GetComponent<ParticleSystem>().Play();
            }

            Destroy(gameObject, deathTime);
        }
        timeToDamage += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if(timeToDamage>invulneravility)
        {
            vida -= damage;
            if(GetComponent<SpriteRenderer>() != null)
            {
                sp.color = Color.red;
            }

            if(GetComponent<AudioSource>() != null)
            {
                aud.pitch = Random.Range(pitch*0.75f, pitch * 1.25f);
                aud.Play();
            }
            Invoke("ChangeBack", 0.1f);
        }
    }
    private void ChangeBack()
    {
        if(sp!=null)
        {
            sp.color =originalColor;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.SendMessage("TakeDamage");
        }
    }
    private void OnDestroy()
    {
        if(head!=null)
        {
            Instantiate(head, transform.position, new Quaternion(0, 0, 0, 0));
        }
        if(corn!=null && corncub!=null)
        {
            if (cornWeight >= 0.9 && Random.Range(0, 100) < cornWeight * 100)
            {
                Instantiate(corncub, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                Instantiate(corncub, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                if (Random.Range(0, 100) < (cornWeight * 100) - 5)
                {
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                    if (Random.Range(0, 100) < (cornWeight * 100) - 10- playerController.sala*2)
                    {
                        Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        if (Random.Range(0, 100) < (cornWeight * 100) - 15- playerController.sala * 2)
                        {
                            Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f),0), new Quaternion(0, 0, 0, 0));
                        }
                    }
                }
            }
            else if (Random.Range(0, 100) < cornWeight * 100)
            {
                Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                if (Random.Range(0, 100) < (cornWeight * 100)-playerController.sala * 2)
                {
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                    if (Random.Range(0, 100) < (cornWeight * 100) - 20 - playerController.sala)
                    {
                        Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        if (cornWeight > 0.5 && Random.Range(0, 100) < (cornWeight * 100) - 10 - playerController.sala * 2)
                        {
                            Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        }
                    }
                }
            }
        }

    }
}
