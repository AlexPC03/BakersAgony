using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBasicLifeSystem
{
    private Camera camara;
    protected bool reseted;
    // Start is called before the first frame update
    protected void StartBoss()
    {
        StartVida();
    }

    // Update is called once per frame
    void Update()
    {
    }


}
