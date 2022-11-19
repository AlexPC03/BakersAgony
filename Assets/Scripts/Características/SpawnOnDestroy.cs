using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject[] spawnList;
    public bool OnlyOne=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(!OnlyOne)
        {
            foreach (var spawn in spawnList)
            {
                Instantiate(spawn,transform.position,new Quaternion(0,0,0,0));
            }
        }
        else
        {
            Instantiate(spawnList[Random.Range(0, spawnList.Length)], transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}
