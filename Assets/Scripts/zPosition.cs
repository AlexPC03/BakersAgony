using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, transform.position.y/10) ;
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.y / 10);

    }
}
