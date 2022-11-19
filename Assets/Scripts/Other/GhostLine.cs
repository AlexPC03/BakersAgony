using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLine : MonoBehaviour
{
    private int actualRoom;
    private GameObject player;
    private Vector3 direction;
    private GameObject point;
    private float time;
    public GameObject ghost;
    public float rate;
    public dir orientation = dir.rand;
    public enum dir
    {
        rand,
        right,
        left
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        actualRoom = player.GetComponent<playerMovement>().sala;
        time = 0;
        point = transform.GetChild(0).gameObject;
        switch(orientation)
        {
            case dir.rand:
                if (transform.position.x>0)
                {
                    direction = transform.position + new Vector3(Random.Range(-0.05f, -0.1f), Random.Range(-0.1f, 0.1f), 0);
                }
                else
                {
                    direction = transform.position + new Vector3(Random.Range(0.05f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                }
                break;
            case dir.right:
                direction = transform.position + new Vector3(1,0,0);
                break;
            case dir.left:
                direction = transform.position + new Vector3(-1, 0, 0);
                break;
        }

        point.transform.position = direction;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>rate)
        {
            GameObject spawnedGhost=Instantiate(ghost,transform.position,new Quaternion (0,0,0,0));
            spawnedGhost.GetComponent<GhostController>().point = point.transform;
            time = 0;

        }

        if (player.GetComponent<playerMovement>().sala != actualRoom)
        {
            Destroy(gameObject);
        }
    }
}
