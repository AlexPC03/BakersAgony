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
            if (coll.tag == "Enemy" || coll.tag == "Boss")
            {
                if(coll.GetComponent<EnemyBasicLifeSystem>()!=null)
                coll.SendMessage("TakeDamage", damage*Player.GetComponent<playerMovement>().attack);
            }
            if(coll.tag!="UnstoppableProyectile")
            {
                if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    if(coll.tag == "Boss")
                    {
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * forceHit * Player.GetComponent<playerMovement>().attack/3, ForceMode2D.Impulse);
                    }
                    else
                    {
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * forceHit * Player.GetComponent<playerMovement>().attack, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    public void Pick()
    {
        Vector3 pos;
        Quaternion rot;
        Transform otherSword;
        otherSword=SwordObj.transform.GetChild(0);
        pos = otherSword.localPosition;
        rot = otherSword.localRotation;

        otherSword.SetParent(gameObject.transform.parent);
        otherSword.transform.localPosition = Vector3.zero;
        transform.SetParent(SwordObj.transform);
        transform.localPosition = pos;
        transform.localRotation = rot;
    }
}
