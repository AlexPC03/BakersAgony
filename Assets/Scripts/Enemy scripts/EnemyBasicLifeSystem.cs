using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicLifeSystem : MonoBehaviour
{
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
        vida = maxVida;
        sp = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(vida<=0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            GetComponent<Collider2D>().enabled = false;
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
            sp.color = Color.red;
            Invoke("ChangeBack", 0.1f);
        }
    }
    private void ChangeBack()
    {
        sp.color = Color.white;
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
            if (cornWeight > 0.9 && Random.Range(0, 100) < cornWeight * 100)
            {
                Instantiate(corncub, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                Instantiate(corncub, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                if (Random.Range(0, 100) < (cornWeight * 100) - 5)
                {
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                    if (Random.Range(0, 100) < (cornWeight * 100) - 10)
                    {
                        Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        if (Random.Range(0, 100) < (cornWeight * 100) - 15)
                        {
                            Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f),0), new Quaternion(0, 0, 0, 0));
                        }
                    }
                }
            }
            else if (Random.Range(0, 100) < cornWeight * 100)
            {
                Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                if (Random.Range(0, 100) < cornWeight * 100)
                {
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                    if (Random.Range(0, 100) < (cornWeight * 100) - 20)
                    {
                        Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        if (cornWeight > 0.5 && Random.Range(0, 100) < (cornWeight * 100) - 10)
                        {
                            Instantiate(corn, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0), new Quaternion(0, 0, 0, 0));
                        }
                    }
                }
            }
        }

    }
}
