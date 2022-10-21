using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMageProyectileMovement : ProyectileBasicSystem
{
    private float timePassed;
    public Vector3 targetPos;
    public bool seeker;
    public bool rotate;
    public float rotateVelocity;
    public float dragForce;
    public float livingTime;
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        ProyectileStart();
        target = GameObject.FindGameObjectWithTag("Player");
        if (!seeker && delayTime == 0)
        {
            rb.drag = dragForce;
            rb.AddForce((targetPos - transform.position).normalized * velocity, ForceMode2D.Impulse);
            Invoke("Dissapear", livingTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate)
        {
            transform.Rotate(0, 0, rotateVelocity * Time.deltaTime);
        }
        if(seeker)
        {
            rb.AddForce((target.transform.position - transform.position).normalized * velocity, ForceMode2D.Force);
            Invoke("Dissapear", livingTime);
        }
    }
}
