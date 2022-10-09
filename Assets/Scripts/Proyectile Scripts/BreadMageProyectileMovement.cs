using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMageProyectileMovement : ProyectileBasicSystem
{
    public Vector3 targetPos;
    public bool seeker;
    public float dragForce;
    public float livingTime;
    // Start is called before the first frame update
    void Start()
    {
        ProyectileStart();
        target = GameObject.FindGameObjectWithTag("Player");
        if(seeker)
        {
            rb.drag = 1f;
            transform.LookAt(targetPos);
        }
        else
        {
            rb.drag = dragForce;
            rb.AddForce((targetPos - transform.position).normalized * velocity, ForceMode2D.Impulse);
            Invoke("Dissapear", livingTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(seeker)
        {
            rb.AddForce((target.transform.position - transform.position).normalized * velocity, ForceMode2D.Force);
            Invoke("Dissapear", livingTime);
        }
    }
}
