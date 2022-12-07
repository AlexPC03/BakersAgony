using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicLifeSystem : MonoBehaviour
{
    private float ghostTime;
    private playerMovement playerController;
    protected Color originalColor;
    public float pitch=1;
    protected AudioSource aud;
    private float timeToDamage = 1;
    private float invulneravility = 0.05f;
    protected SpriteRenderer sp;
    public GameObject corn;
    public GameObject corncub;
    [Range(0, 1)]
    public float cornWeight;
    public GameObject head;
    public GameObject explosion;
    public bool ghost=false;
    [Header("LifeParameters")]
    public bool dontScaleLife;
    public float maxVida;
    public float deathTime;
    public float vida;

    // Start is called before the first frame update
    public void StartVida()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        if(!dontScaleLife)
        {
            maxVida = maxVida + playerController.sala;
        }
        vida = maxVida;
        if(ghost && gameObject.tag!="Boss")
        {
            sp=GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            sp = GetComponent<SpriteRenderer>();
        }
        aud = GetComponent<AudioSource>();
        if(sp!=null)
        {
            originalColor = sp.color;
        }
        
    }

    void Start()
    {
        StartVida();
    }
    // Update is called once per frame
    void Update()
    {

        timeToDamage += Time.deltaTime;
    }

    public virtual void TakeDamage(float damage)
    {
        if(timeToDamage>invulneravility && gameObject.layer!=6)
        {
            vida -= damage;
            if(sp != null && !ghost)
            {
                sp.color = Color.red;
            }
            else if(sp != null && ghost)
            {
                sp.color = new Color (1,0,0,0.75f);
            }
            Invoke("ChangeBack", 0.1f);
            timeToDamage = 0;
        }
        if(aud != null)
        {
            aud.pitch = Random.Range(pitch*0.75f, pitch * 1.25f);
            aud.Play();
        }
        if (vida <= 0)
        {
            if (gameObject.tag != "SpecialEnemy")
            {
                if (GetComponent<Rigidbody2D>() != null)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().angularVelocity = 0f;
                    GetComponent<Rigidbody2D>().bodyType =RigidbodyType2D.Static;
                }
                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
            }
            if (GetComponent<ParticleSystem>() != null)
            {
                GetComponent<ParticleSystem>().Play();
            }
            if (ghost && sp != null)
            {
                ghostTime += Time.deltaTime;
                sp.color = Color.Lerp(originalColor, Color.clear, ghostTime / deathTime);
                if (sp.color == Color.clear)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject, deathTime);
            }
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
        if(collision.tag=="Player" && gameObject.layer != 6)
        {
            collision.SendMessage("TakeDamage");
        }
    }
    private void OnDestroy()
    {
        if(GetComponent<SpawnOnDestroy>()==null && playerController.demonMask && explosion!=null && Random.Range(0,2)==1)
        {
            Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        }
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
