using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBasicLifeSystem
{
    private Camera camara;
    private bool reseted;
    // Start is called before the first frame update
    protected void StartBoss()
    {
        StartVida();
        camara = Camera.main;
        camara.orthographicSize *= 1.5f;
        reseted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(vida<=0 && reseted)
        {
            ResetCamera();
        }
    }

    protected void SetCamera()
    {
        camara.orthographicSize *= 1.5f;
    }
    protected void ResetCamera()
    {
        reseted = true;
        camara.orthographicSize /= 1.5f;
    }

}
