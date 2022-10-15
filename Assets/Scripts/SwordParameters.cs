using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParameters : SwordKnockback
{
    // Start is called before the first frame update
    void Start()
    {
        StartSword();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (Mathf.Abs(Swordspeed) > neededSpeed)
        {
            if (coll.tag == "Enemy")
            {
                coll.SendMessage("TakeDamage", damage);
            }
            if(coll.tag!="UnstoppableProyectile")
            {
                if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * forceHit, ForceMode2D.Impulse);

                }
            }
        }
    }
}
