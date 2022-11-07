using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CursorGamepadController : StandaloneInputModule
{
    private Gamepad gamepad;
    private Vector2 actualMousePos = Vector2.zero;
    public bool mainMenu;

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        if(gamepad!=null)
        {
            if(!mainMenu)
            {
                if (Time.timeScale == 0)
                {
                    actualMousePos = new Vector2(actualMousePos.x + Input.GetAxis("Horizontal") * 5, actualMousePos.y + Input.GetAxis("Vertical") * 5);
                    Mouse.current.WarpCursorPosition(new Vector2(actualMousePos.x, Screen.height - actualMousePos.y));
                    if (gamepad.aButton.wasPressedThisFrame || gamepad.bButton.wasPressedThisFrame || gamepad.crossButton.wasPressedThisFrame)
                    {
                        ClickAt(actualMousePos, true);
                    }

                    if (gamepad.aButton.wasReleasedThisFrame || gamepad.bButton.wasReleasedThisFrame || gamepad.crossButton.wasReleasedThisFrame)
                    {
                        ClickAt(actualMousePos, false);
                    }
                    Cursor.visible = true;
                }
                else if(Time.timeScale!=0)
                {
                    Cursor.visible= false;
                }
            }
            else
            {
                actualMousePos = new Vector2(actualMousePos.x + Input.GetAxis("Horizontal") * 5, actualMousePos.y + Input.GetAxis("Vertical") * 5);
                Mouse.current.WarpCursorPosition(new Vector2(actualMousePos.x, Screen.height-actualMousePos.y));
                if (gamepad.aButton.wasPressedThisFrame || gamepad.bButton.wasPressedThisFrame || gamepad.crossButton.wasPressedThisFrame)
                {
                    ClickAt(actualMousePos, true);
                }
                if (gamepad.aButton.wasReleasedThisFrame || gamepad.bButton.wasReleasedThisFrame || gamepad.crossButton.wasReleasedThisFrame)
                {
                    ClickAt(actualMousePos, false);
                }
                Cursor.visible = true;           
            }
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void ClickAt(Vector2 pos, bool pressed)
    {
        Input.simulateMouseWithTouches = true;
        var pointerData = GetTouchPointerEventData(new Touch()
        {
            position = pos,
        }, out bool b, out bool bb);

        ProcessTouchPress(pointerData, pressed, !pressed);
    }
}
