using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationModifier : MonoBehaviour
{
    private Collider2D[] results;
    public GameObject[] objects;
    public int mediaObj;
    // Start is called before the first frame update
    void Start()
    {
        float a = Random.Range(mediaObj - mediaObj / 5, mediaObj + mediaObj / 5);

        for (int i=0;i<a;i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-12f, 12f), Random.Range(-12f, 12f), 0);
            Physics2D.OverlapArea(transform.position - new Vector3(-0.5f, -0.5f, 0), transform.position - new Vector3(0.5f, 0.5f, 0),7);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
