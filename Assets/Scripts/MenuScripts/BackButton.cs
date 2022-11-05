using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BackButton : MonoBehaviour
{
    private Gamepad gamepad;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (Time.timeScale == 0f && (gamepad.startButton.wasPressedThisFrame || gamepad.selectButton.wasPressedThisFrame))
            {
                UnpauseGame();
                OptionsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }
        else
        {
            if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Escape))
            {
                UnpauseGame();
                OptionsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }

    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
