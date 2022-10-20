using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    public Collider2D coll;
    public GameObject[] rooms;
    public GameObject rewardRoom;
    public GameObject shopRoom;
    public bool boss;
    public GameObject bossRoom1;
    public GameObject bossRoom2;
    public GameObject bossRewardRoom;

    public GameObject nextRoom;
    public bool initiated;
    public bool enemies;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        initiated = false;
        nextRoom = rooms[Random.Range(0, rooms.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y-transform.position.y>75)
        {
            deleteObjects();
        }
    }

    void FixedUpdate()
    {
        enemies = GameObject.FindWithTag("Enemy") != null || GameObject.FindWithTag("Boss") != null;
        if (GameObject.FindWithTag("Enemy")!=null || GameObject.FindWithTag("Boss") != null || initiated)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
        anim.SetBool("Closed", coll.enabled);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==player && !initiated)
        {
            initiated = true;
            if(boss)
            {
                Instantiate(bossRewardRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (player.GetComponent<playerMovement>().sala == 34)
            {
                Instantiate(bossRoom2, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if(player.GetComponent<playerMovement>().sala==17)
            {
                Instantiate(bossRoom1, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (player.GetComponent<playerMovement>().sala % 8 == 0)
            {
                Instantiate(shopRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if(player.GetComponent<playerMovement>().sala % 3 == 0)
            {
                Instantiate(rewardRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }

            else
            {
                Instantiate(nextRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            player.SendMessage("nextRoom");
        }
    }
    private void deleteObjects()
    {
        GameObject obj=transform.root.gameObject;
        Destroy(obj);
    }
}
