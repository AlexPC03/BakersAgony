using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    public int maskID;
    private AudioSource aud;
    private GameObject MaskObj;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        MaskObj = GameObject.Find("MaskObject");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pick()
    {
        if (aud != null)
        {
            aud.Play();
        }
        Vector3 pos;
        Vector3 scale;
        Transform otherMask;
        otherMask = MaskObj.transform.GetChild(0);
        pos = otherMask.localPosition;
        scale = otherMask.localScale;

        otherMask.SetParent(gameObject.transform.parent);
        otherMask.transform.localPosition = Vector3.zero;
        transform.SetParent(MaskObj.transform);
        transform.localPosition = pos;
        transform.localScale=scale;
    }
}
