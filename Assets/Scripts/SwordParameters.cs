using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParameters : SwordKnockback
{
    private AudioSource aud;
    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        StartSword();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (Mathf.Abs(Swordspeed) > neededSpeed && coll.tag != "Decoration")
        {
            if (coll.tag == "Enemy" || coll.tag == "Boss" || coll.tag == "SpecialEnemy")
            {
                if (coll.GetComponent<EnemyBasicLifeSystem>() != null)
                    coll.SendMessage("TakeDamage", damage * (Player.GetComponent<playerMovement>().attack * Player.GetComponent<playerMovement>().attackMultiplier));
            }
            if (coll.tag != "UnstoppableProyectile" )
            {
                if (coll.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    if (coll.tag == "Boss")
                    {
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * forceHit * (Player.GetComponent<playerMovement>().attack / 5), ForceMode2D.Impulse);
                    }
                    else
                    {
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce((coll.transform.position - Player.transform.position).normalized * forceHit * (Player.GetComponent<playerMovement>().attack * 0.75f), ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    public void Pick()
    {
        if(aud!=null)
        {
            aud.pitch = Random.Range(0.9f, 1.1f);
            aud.clip = clip[Random.Range(0, clip.Length)];
            aud.Play();
        }
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
