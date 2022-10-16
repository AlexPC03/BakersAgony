using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private GameObject player;
    public bool used = false;
    public int times;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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
                player.SendMessage("RecoverLife");
            }
        }

        used = true;
    }
}
