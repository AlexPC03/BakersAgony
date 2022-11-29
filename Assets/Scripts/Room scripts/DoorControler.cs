using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    private Animator anim;
    public Collider2D coll;
    public GameObject[] rooms;
    public GameObject rewardRoom;
    public GameObject shopRoom;
    public bool boss;
    public bool bossReward;
    public GameObject bossRoom1;
    public GameObject bossRoom1_2;
    public GameObject bossRoom2;
    public GameObject bossRoom2_2;
    public GameObject bossRoom3;
    public GameObject bossRewardRoom;
    public GameObject finalRewardRoom;

    public GameObject nextRoom;
    public bool initiated;
    public bool enemies;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
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
        if (enemies || initiated)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
        anim.SetBool("Closed", coll.enabled);

    }

    const int salaBoss1 = 9;
    const int salaBoss2 = 17;
    const int salaBoss3 = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==player && !initiated)
        {
            if (!enemies)
            {
                aud.Play();
            }
            initiated = true;
            if(bossReward)
            {
                if(!player.GetComponent<playerMovement>().endless)
                {
                    if(player.GetComponent<playerMovement>().zona==playerMovement.zone.entrada)
                    {
                        player.GetComponent<playerMovement>().zona = playerMovement.zone.pastelerķa;
                    }
                    else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pastelerķa)
                    {
                        player.GetComponent<playerMovement>().zona = playerMovement.zone.horneadores;
                    }
                    else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
                    {
                        player.GetComponent<playerMovement>().zona = playerMovement.zone.entrada;
                    }
                }

            }
            if(player.GetComponent<playerMovement>().sala==salaBoss3+1 && !player.GetComponent<playerMovement>().endless)
            {
                Instantiate(finalRewardRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (boss)
            {
                Instantiate(bossRewardRoom, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if(player.GetComponent<playerMovement>().sala % salaBoss3==0 )
            {
                Instantiate(bossRoom3, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (player.GetComponent<playerMovement>().sala % salaBoss2 == 0)
            {
                switch (Random.Range(0,2))
                {
                    case 0:
                        Instantiate(bossRoom2, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
                        break;
                    case 1:
                        Instantiate(bossRoom2_2, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
                        break;
                }
            }
            else if(player.GetComponent<playerMovement>().sala % salaBoss1 == 0)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        Instantiate(bossRoom1, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
                        break;
                    case 1:
                        Instantiate(bossRoom1_2, transform.position + new Vector3(0, 21, 0), new Quaternion(0, 0, 0, 0));
                        break;
                }
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
