using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    private GameObject player;
    public Sprite[] defaultWallList;
    public Sprite[] sugarWallList;
    public Sprite[] fungiWallList;
    public Sprite[] burnedWallList;
    public bool transparent;
    public GameObject sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(Random.Range(0,2)==0)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(player.GetComponent<playerMovement>().zona==playerMovement.zone.entrada)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = defaultWallList[Random.Range(0, defaultWallList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pastelería)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = sugarWallList[Random.Range(0, sugarWallList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.hongos)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = fungiWallList[Random.Range(0, fungiWallList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = burnedWallList[Random.Range(0, burnedWallList.Length)];
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.CompareTag("Player") || coll.CompareTag("Enemy") || coll.CompareTag("SpecialEnemy") || coll.CompareTag("Boss") || coll.CompareTag("Helper") && transparent)
        {
            foreach(SpriteRenderer spr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b,0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.CompareTag("Player") || coll.CompareTag("Enemy") || coll.CompareTag("SpecialEnemy") || coll.CompareTag("Boss") || coll.CompareTag("Helper"))
        {
            foreach (SpriteRenderer spr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);
            }
        }
    }
}
