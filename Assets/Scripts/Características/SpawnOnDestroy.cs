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
                GameObject obj= Instantiate(spawn,transform.position,new Quaternion(0,0,0,0));
                if (obj.GetComponent<BreadMageProyectileMovement>() != null)
                {
                    obj.GetComponent<BreadMageProyectileMovement>().targetPos = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
                }
                else if (obj.GetComponent<BumeranProyectileMovement>() != null)
                {
                    obj.GetComponent<BumeranProyectileMovement>().targetPos = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
                }
            }
        }
        else
        {
            GameObject obj=Instantiate(spawnList[Random.Range(0, spawnList.Length)], transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}
