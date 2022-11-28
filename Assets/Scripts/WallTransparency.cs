using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    private GameObject player;
    public Sprite[] defaultWallList;
    public Sprite[] sugarWallList;
    public Sprite[] burnedWallList;
    public bool transparent;
    public GameObject sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player.GetComponent<playerMovement>().zona==playerMovement.zone.entrada)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = defaultWallList[Random.Range(0, defaultWallList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pasteler�a)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = sugarWallList[Random.Range(0, defaultWallList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = burnedWallList[Random.Range(0, defaultWallList.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {

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
