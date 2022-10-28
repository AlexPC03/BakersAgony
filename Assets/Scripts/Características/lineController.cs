using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour
{
    private LineRenderer line;

    public GameObject conectedObj;
    public GameObject conectedObj2;
    // Start is called before the first frame update
    void Start()
    {
        line=GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(line!=null)
        {
            line.SetPosition(0, conectedObj.transform.position);
            line.SetPosition(1, conectedObj2.transform.position);
        }
    }
}
