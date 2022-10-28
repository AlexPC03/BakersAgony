using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObjectController : MonoBehaviour
{
    private GameObject Player;
    public GameObject sword;
    private SwordParameters parameters;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(sword.GetComponent<SwordParameters>()!=null)
        {
            parameters = sword.GetComponent<SwordParameters>();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(sword.GetComponent<SwordParameters>() != null)
        {
            if (Mathf.Abs(parameters.Swordspeed) > parameters.neededSpeed)
            {
                if (coll.tag == "Enemy" || coll.tag == "Boss" || coll.tag == "SpecialEnemy")
                {
                    if (coll.GetComponent<EnemyBasicLifeSystem>() != null)
                        coll.SendMessage("TakeDamage", parameters.damage * Player.GetComponent<playerMovement>().attack);
                }
                if (coll.tag != "UnstoppableProyectile")
                {
                    if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        if (coll.tag == "Boss")
                        {
                            coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * parameters.forceHit * Player.GetComponent<playerMovement>().attack / 3, ForceMode2D.Impulse);
                        }
                        else
                        {
                            coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * parameters.forceHit * Player.GetComponent<playerMovement>().attack, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }
    }
}
