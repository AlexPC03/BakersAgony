using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class CutSceneController : MonoBehaviour
{
    private Gamepad gamepad;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {
            if (Input.anyKey)
            {
                anim.speed = 15;
            }
            else
            {
                anim.speed = 1;
            }
        }
        else
        {
            if (gamepad.aButton.isPressed || gamepad.bButton.isPressed || gamepad.crossButton.isPressed)
            {
                anim.speed = 15;
            }
            else
            {
                anim.speed = 1;
            }
        }



    }
    public void ChangeSceneToMain()
    {
        anim.speed = 1;

        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeSceneToMenu()
    {
        anim.speed = 1;

        SceneManager.LoadScene("MainMenu");
    }
}
