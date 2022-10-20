using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour
{
    public GameObject objectToInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectToInstantiate, transform.position, new Quaternion(0, 0, 0,0));
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
