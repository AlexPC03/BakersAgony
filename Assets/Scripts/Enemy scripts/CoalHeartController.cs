using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalHeartController : EnemyBasicLifeSystem
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( vida <= 0 )
        {
            wall.SendMessage("TakeDamage", 1000);
        }
    }
}
