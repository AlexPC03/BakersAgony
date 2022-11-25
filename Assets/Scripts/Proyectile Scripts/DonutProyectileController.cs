using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutProyectileController : ProyectileBasicSystem
{
    public bool randomTarget;
    // Start is called before the first frame update
    void Start()
    {
        ProyectileStart();
        if(randomTarget)
        {
            rb.velocity = (transform.position+ new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f),0)).normalized * velocity;
        }
        else if(target!=null)
        {
            rb.velocity = (target.transform.position - transform.position).normalized * velocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * velocity;
    }

    public void DownCollision()
    {
        rb.velocity=new Vector2 (rb.velocity.x,-rb.velocity.y);
    }

    public void SideCollision()
    {
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
    }
}
