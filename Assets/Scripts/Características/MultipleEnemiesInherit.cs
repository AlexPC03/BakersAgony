using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleEnemiesInherit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag=="SpecialEnemy")
        {
            foreach(Transform obj in transform.GetComponentsInChildren<Transform>())
            {
                if(obj.gameObject.tag != "SpecialEnemy")
                {
                    obj.gameObject.tag = "SpecialEnemy";
                }
                if(obj.gameObject.GetComponent<DestroyByDistance>()==null)
                {
                    obj.gameObject.AddComponent<DestroyByDistance>();
                }
            }
        }
    }
}
