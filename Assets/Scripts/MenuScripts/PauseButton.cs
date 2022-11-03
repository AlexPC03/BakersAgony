using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private Gamepad gamepad;
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gamepad = Gamepad.current;
        if(gamepad!=null)
        {
            if (Time.timeScale == 1f && (gamepad.startButton.wasPressedThisFrame || gamepad.selectButton.wasPressedThisFrame)) 
            {
                PauseGame();
                OptionsMenu.SetActive(true);
                MainMenu.SetActive(false);
            }
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
