using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumeranProyectileMovement : ProyectileBasicSystem
{
    public Vector3 targetPos;
    public Vector3 initialPos;
    public float catchDistance=2;
    public float rotateVelocity;
    public bool randomInitialForce;
    public float angle;
    public bool returning;
    // Start is called before the first frame update
    void Start()
    {
        ProyectileStart();
        initialPos = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        if (randomInitialForce)
        {
            rb.AddForce((Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * (targetPos - transform.position)).normalized * velocity * 5, ForceMode2D.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateVelocity * Time.deltaTime);
        if((targetPos - transform.position).magnitude< catchDistance/2)
        {
            returning = true;
        }
        if(!returning)
        {
            rb.AddForce((targetPos - transform.position).normalized * velocity, ForceMode2D.Force);
        }
        else
        {
            rb.AddForce((initialPos - transform.position).normalized * velocity, ForceMode2D.Force);
        }
        if(returning&& (initialPos - transform.position).magnitude< catchDistance)
        {
            Dissapear();
        }
    }
}
