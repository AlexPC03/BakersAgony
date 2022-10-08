using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zPosition : MonoBehaviour
{
    public float difference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, (transform.position.y+difference)/ 10) ;
    }
}
