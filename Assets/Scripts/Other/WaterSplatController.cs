using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplatController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BreadEnemy>()!=null)
        {
            if (collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag == "SpecialEnemy")
            {
                collision.gameObject.SendMessage("TakeDamage", 25f);
            }
        }
    }
}
