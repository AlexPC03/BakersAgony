using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private GameObject player;
    private playerMovement playerHealth;
    public GameObject corn;
    public bool used = false;
    public int times;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(used)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Pick()
    {

        if(!used)
        {
            for(int i=0;i<times;i++)
            {
                if (playerHealth.health < playerHealth.maxLife)
                {
                    player.SendMessage("RecoverLife");
                }
                else
                {
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0), new Quaternion(0, 0, 0, 0));
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
                    Instantiate(corn, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
                    if(Random.Range(0,2)==1)
                    {
                        Instantiate(corn, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), new Quaternion(0, 0, 0, 0));
                    }
                }
            }  
        }

        used = true;
    }
}
