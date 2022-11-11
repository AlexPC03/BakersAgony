using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRandomRotation : MonoBehaviour
{
    public float speed;
    public bool random=true;
    // Start is called before the first frame update
    void Start()
    {
        if(random)
        {
            speed = Random.Range(15f, 90f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * speed);
    }
}
