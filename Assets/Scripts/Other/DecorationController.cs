using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{
    private ParticleSystem part;
    public GameObject[] Spawnlist;
    public Sprite changeSprite;
    public bool dissapear;
    public float dissapearTime;
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<ParticleSystem>()!=null)
        {
            part= GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SpecialEnemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Helper")
        {
            if(part!=null)
            {
                part.Play();
            }
            if (changeSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = changeSprite;
            }
            if (dissapear)
            {
                Invoke("Dissapear", dissapearTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SpecialEnemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Helper")
        {
            if (part != null)
            {
                part.Play();
            }
            if (changeSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = changeSprite;
            }
            if (dissapear)
            {
                Invoke("Dissapear", dissapearTime);
            }
        }
    }

    private void Dissapear()
    {
        foreach(GameObject spawn in Spawnlist)
        {
            Instantiate(spawn,transform.position,new Quaternion (0,0,0,0));
        }
        Destroy(gameObject, destroyTime);
    }
}
