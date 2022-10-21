using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Sprite[] wallList;
    public bool transparent;
    
    // Start is called before the first frame update
    void Start()
    {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = wallList[Random.Range(0,wallList.Length)];       
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (SpriteRenderer spr in transform.GetComponentsInChildren<SpriteRenderer>())
        //{
        //    spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 255f);
        //}
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if((coll.tag=="Player" || coll.tag=="Enemy")&& transparent)
        {
            foreach(SpriteRenderer spr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b,0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.CompareTag("Player") || coll.CompareTag("Enemy"))
        {
            foreach (SpriteRenderer spr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f);
            }
        }
    }
}
