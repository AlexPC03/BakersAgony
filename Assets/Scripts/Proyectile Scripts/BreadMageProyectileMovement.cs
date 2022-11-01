using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMageProyectileMovement : ProyectileBasicSystem
{
    public Vector3 targetPos;
    public bool seeker;
    public bool rotate;
    public bool lookAt;
    public float rotateVelocity;
    public float dragForce;
    public float livingTime;
    // Start is called before the first frame update
    void Start()
    {
        ProyectileStart();
        target = GameObject.FindGameObjectWithTag("Player");
        if (!seeker)
        {
            rb.drag = dragForce;
            rb.AddForce((targetPos - transform.position).normalized * velocity, ForceMode2D.Impulse);
            Invoke("Dissapear", livingTime);
            if(lookAt)
            {
                transform.right = -(targetPos - transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate &&!lookAt)
        {
            transform.Rotate(0, 0, rotateVelocity * Time.deltaTime);
        }

        if(seeker)
        {
            if (lookAt)
            {
                transform.right = -(target.transform.position - transform.position);
            }
            rb.AddForce((target.transform.position - transform.position).normalized * velocity, ForceMode2D.Force);
            Invoke("Dissapear", livingTime);
        }
    }
}
