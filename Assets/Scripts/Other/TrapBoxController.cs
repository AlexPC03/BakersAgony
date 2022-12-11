using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBoxController : MonoBehaviour
{
    private float timePassed=0;
    public GameObject proyectile;
    public bool allDirections;
    public Vector3[] directions;
    public float timeBetweenShoots;

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed+=Time.deltaTime;
        if(timePassed>timeBetweenShoots )
        {
            if(allDirections)
            {
                foreach(Vector3 direction in directions)
                {
                    ShootOne(direction); 
                }
            }
            else
            {
                ShootOne(directions[Random.Range(0, directions.Length)]);
            }
            timePassed = 0;
        }
    }

    public void ShootOne(Vector3 pos)
    {
        GameObject obj= Instantiate(proyectile,transform.position,new Quaternion (0,0,0,0));
        if(obj.GetComponent<BreadMageProyectileMovement>()!=null)
        {
            if (!obj.GetComponent<BreadMageProyectileMovement>().seeker)
            {
                obj.GetComponent<BreadMageProyectileMovement>().targetPos = transform.position + pos;
            }
        }
    }
}
