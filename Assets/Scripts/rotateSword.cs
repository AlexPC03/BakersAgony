using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rotateSword : MonoBehaviour
{
    private Vector2 lastStickL=Vector2.zero;
    public float angVelocity;

    public GameObject player;

    public float force; // spin speed
    float angle1=0; // angle1 will be negative to normal angle
    public Rigidbody2D rb;
    float angle=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector3(player.transform.position.x, player.transform.position.y-0.3f, player.transform.position.z) ;
        force = GameObject.Find("SwordObject").GetComponentInChildren<SwordParameters>().rotationForce;
        rb.angularDrag= GameObject.Find("SwordObject").GetComponentInChildren<SwordParameters>().deceleration;
        angVelocity = rb.angularVelocity;
    }

    void FixedUpdate()
    {
        faceMouse();
    }

    void faceMouse()
    {
        
        Gamepad gamepad = Gamepad.current;

        if (gamepad == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y) * 180 / Mathf.PI; //Get mouse angle
        }
        else if (gamepad != null)
        {
            Vector2 stickL = gamepad.rightStick.ReadValue();           
            if (stickL!=Vector2.zero)
            {
                lastStickL = stickL;
            }
            angle = Mathf.Atan2(player.transform.position.x+ lastStickL.x - transform.position.x, player.transform.position.y+ lastStickL.y - transform.position.y) * 180 / Mathf.PI; //Get joystick angle
        }
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
