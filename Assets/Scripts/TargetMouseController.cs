using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TargetMouseController : MonoBehaviour
{
    private GameObject player;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;

        if (GetComponent<SpriteRenderer>().enabled == true)
        {
            if (gamepad == null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePos;
            }
            else
            {
                Vector2 stickL = gamepad.rightStick.ReadValue();
                transform.position += new Vector3(stickL.x * 0.075f, stickL.y * 0.075f, 0);
                if ((gamepad.aButton.wasPressedThisFrame || gamepad.bButton.wasPressedThisFrame || gamepad.crossButton.wasPressedThisFrame) && GetComponent<SpriteRenderer>().enabled == true)
                {
                    transform.position = player.transform.position + new Vector3(0, 0, 0);
                }
            }
        }
    }
}
