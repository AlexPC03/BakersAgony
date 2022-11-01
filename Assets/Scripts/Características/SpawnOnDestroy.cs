using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject[] spawnList;
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
        foreach (var spawn in spawnList)
        {
            Instantiate(spawn,transform.position,new Quaternion(0,0,0,0));
        }
    }
}
