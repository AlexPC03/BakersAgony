using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour
{
    private LineRenderer line;
    private SpringJoint2D spring;

    public GameObject conectedObj;
    public Vector3 anchorPosition;
    // Start is called before the first frame update
    void Start()
    {
        line=GetComponent<LineRenderer>();
        spring=GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(line!=null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, conectedObj.transform.position+anchorPosition);
        }
        if(spring!=null)
        {
            spring.anchor = Vector2.zero;
            spring.connectedAnchor = conectedObj.transform.position + anchorPosition;
        }
    }
}
