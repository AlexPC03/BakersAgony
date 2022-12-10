using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileBasicSystem : MonoBehaviour
{
    private float t = 0;
    public GameObject target;
    protected Rigidbody2D rb;


    public float destroyTime;
    public bool friendlyFire;
    public float friendlyDamage=20;
    public bool destroyOnHit;
    public bool ghost;
    public bool donut;

    public float velocity;
    // Start is called before the first frame update
    protected void ProyectileStart()
    {
        rb = GetComponent<Rigidbody2D>();
        if(GetComponent<AudioSource>()!=null)
        {
            GetComponent<AudioSource>().pitch = GetComponent<AudioSource>().pitch * Random.Range(0.75f, 1.25f);
        }
    }

    private void Update()
    {
        if (t <= 0.5f)
        {
            t += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6 && t>0.5f && !collision.isTrigger && !ghost && !donut)
        {
            Dissapear();
        }
        if (collision.tag == "Player")
        {
            collision.SendMessage("TakeDamage");
            if(!destroyOnHit)
            {
                Dissapear();
            }
        }
        if ((collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag == "SpecialEnemy") && friendlyFire && collision.GetComponent<ProyectileInmunity>()==null && collision.GetComponent<EnemyBasicLifeSystem>() != null)
        {
            collision.SendMessage("TakeDamage", friendlyDamage);
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * (friendlyDamage/5),ForceMode2D.Impulse);
        }
    }

    protected void Dissapear()
    {
        if(GetComponent<Collider2D>()!=null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        Destroy(gameObject, destroyTime);
    }
}
