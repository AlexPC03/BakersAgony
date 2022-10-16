using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    private Animator anim;
    public Collider2D coll;
    public GameObject[] rooms;
    public GameObject nextRoom;
    public bool initiated;
    public bool enemies;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initiated = false;
        nextRoom = rooms[Random.Range(0, rooms.Length)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemies = GameObject.FindWithTag("Enemy") != null;
        if (GameObject.FindWithTag("Enemy")!=null)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
        anim.SetBool("Closed", enemies);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player" && !initiated)
        {
            initiated = true;
            Instantiate(nextRoom,transform.position+new Vector3(0,21,0),new Quaternion(0,0,0,0));
        }
    }

}
