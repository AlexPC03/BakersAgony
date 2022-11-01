using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBySpeed : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        sprite.Rotate(new Vector3(0, 0, 1), Time.deltaTime * -rb.velocity.x*100);
    }
}
