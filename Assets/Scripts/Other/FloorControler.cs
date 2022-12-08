using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControler : MonoBehaviour
{
    private GameObject player;
    public Sprite defaultFloor;
    public Sprite sugarFloor;
    public Sprite fungiFloor;
    public Sprite burnedFloor;
    public GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Random.Range(0, 2) == 0)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (player.GetComponent<playerMovement>().zona == playerMovement.zone.entrada)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = defaultFloor;
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pastelería)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = sugarFloor;
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.hongos)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = fungiFloor;
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = burnedFloor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
