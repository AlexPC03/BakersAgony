using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifirController : MonoBehaviour
{
    public GameObject[] goodMod;
    public GameObject[] neutralMod;
    public GameObject[] badMod;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0,3)==0)
        {
            int a = Random.Range(0, 3);
            if (a==0)
            {
                Instantiate(neutralMod[Random.Range(0, neutralMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (a == 1)
            {
                Instantiate(goodMod[Random.Range(0, neutralMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (a == 2)
            {
                Instantiate(badMod[Random.Range(0, neutralMod.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
