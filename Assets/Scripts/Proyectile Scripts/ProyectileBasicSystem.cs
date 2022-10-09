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


    public float velocity;
    // Start is called before the first frame update
    protected void ProyectileStart()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (collision.tag == "Enemy" && friendlyFire)
        {
            collision.SendMessage("TakeDamage", 10);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * 5, ForceMode2D.Impulse);
        }
    }

    protected void Dissapear()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, destroyTime);
    }
}
