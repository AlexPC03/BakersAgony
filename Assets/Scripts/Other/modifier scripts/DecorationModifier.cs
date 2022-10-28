using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationModifier : MonoBehaviour
{
    public GameObject[] objects;
    public bool derivate;
    public int mediaObj;
    private float a;
    // Start is called before the first frame update
    void Start()
    {
        if(derivate)
        {
            a = Random.Range(mediaObj - mediaObj / 5, mediaObj + mediaObj / 5);
        }
        else
        {
            a = mediaObj;
        }
        

        for (int i=0;i<a;i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-12f, 12f), Random.Range(-12f, 12f), 0);
            if(Physics2D.OverlapArea(pos - new Vector3(-0.75f, -0.75f, 0), pos - new Vector3(0.75f, 0.75f, 0),7)==null)
            {
                GameObject obj=Instantiate(objects[Random.Range(0, objects.Length)], pos, new Quaternion(0, 0, 0, 0));

            }
            else
            {
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
