using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBasicLifeSystem
{
    public float newScale;
    protected bool reseted;
    // Start is called before the first frame update
    protected void StartBoss()
    {
        StartVida();
    }

}
