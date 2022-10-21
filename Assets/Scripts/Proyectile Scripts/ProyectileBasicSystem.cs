using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileBasicSystem : MonoBehaviour
{
    public GameObject target;
    protected Rigidbody2D rb;

    public float destroyTime;
    public bool friendlyFire;
    public bool destroyOnHit;
    public bool ghost;


    public float velocity;
    // Start is called before the first frame update
    protected void ProyectileStart()
    {
        rb = GetComponent<Rigidbody2D>();
        if(ghost)
        {
            Physics2D.IgnoreLayerCollision(10, 6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            Dissapear();
        }
        if (collision.tag == "Player")
        {
            collision.SendMessage("TakeDamage");
            Dissapear();
        }
        if ((collision.tag == "Enemy" || collision.tag == "Boss") && friendlyFire && collision.GetComponent<ProyectileInmunity>()==null)
        {
            collision.SendMessage("TakeDamage", 10);
        }
    }

    protected void Dissapear()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, destroyTime);
    }
}
