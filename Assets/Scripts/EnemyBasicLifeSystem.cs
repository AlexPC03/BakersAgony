using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicLifeSystem : MonoBehaviour
{
    [Header("LifeParameters")]
    public float maxVida;
    public float deathTime;


    public float vida;
    // Start is called before the first frame update
    public void StartVida()
    {
        vida = maxVida;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(vida<=0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, deathTime);
        }
    }

    public void TakeDamage(float damage)
    {
        vida -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.SendMessage("TakeDamage");
        }
    }
}
