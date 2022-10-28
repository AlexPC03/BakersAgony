using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compoundedSwordController : MonoBehaviour
{
    public Collider2D col;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(col!=null)
        {
            col.offset = obj.transform.localPosition;
        }
    }
}
