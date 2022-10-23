using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifirController : MonoBehaviour
{
    private GameObject player;

    public GameObject[] goodMod;
    public GameObject[] neutralMod;
    public GameObject[] badMod;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (Random.Range(0f,1f)>=0.5f)
        {
            int a = Random.Range(0, 3);
            if (a==0)
            {
                Instantiate(neutralMod[Random.Range(0, neutralMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (a == 1)
            {
                Instantiate(goodMod[Random.Range(0, goodMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (a == 2)
            {
                if(player.GetComponent<playerMovement>().sala >= 12)
                {
                    Instantiate(badMod[Random.Range(0, badMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
                }
                else
                {
                    Instantiate(neutralMod[Random.Range(0, neutralMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
