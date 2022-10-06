using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSword : MonoBehaviour
{
    public float angVelocity;

    public GameObject player;

    public float force; // spin speed
    float angle1; // angle1 will be negative to normal angle
    public Rigidbody2D rb;
    float angle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        angVelocity = rb.angularVelocity;
    }

    void FixedUpdate()
    {
        faceMouse();
    }

    void faceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y) * 180 / Mathf.PI; //Get mouse angle
        rb.rotation %= 360; 
        angle = (angle + rb.rotation); // Sum up rigidbody and mouse angle
        if (angle < 0) angle1 = 360.0f + angle;
        else angle1 = 360.0f - angle; // calculates negative angle
        if (Mathf.Abs(angle) > Mathf.Abs(angle1) && angle < 0)
            angle = angle1;
        if (Mathf.Abs(angle) > Mathf.Abs(angle1) && angle > 0)
            angle = angle1 * -1; // from my testing i found out that by writing these ifs rigid body stops doing awkward 360 turnadounds and spins trough closest path to mouse
        rb.AddTorque(-angle / 180 * (force*10));
    }
}
