using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBasicLifeSystem
{
    private Camera camara;
    private float initialScale;
    public float newScale;
    protected bool reseted;
    // Start is called before the first frame update
    protected void StartBoss()
    {
        StartVida();
        camara = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camara != null)
        {
            initialScale = camara.orthographicSize;
            if (newScale != 0 && vida>0)
            {
                camara.orthographicSize = newScale;
            }
        }
    }

    private void OnDestroy()
    {
        if(camara!=null)
        {
            camara.orthographicSize = initialScale;
        } 
    }

}
