using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookController : MonoBehaviour
{
    private GameObject progresController;

    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        progresController = GameObject.Find("ProgressControl");
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad pad = Gamepad.current;
        if(pad==null)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
        else
        {
            if (pad.startButton.wasPressedThisFrame||pad.selectButton.wasPressedThisFrame)
            {
                Back();
            }
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void ChoosePage(string name)
    {
        foreach(GameObject pag in pages)
        {
            if(pag.GetComponent<PageController>().pageName!=name)
            {
                pag.SetActive(false);
            }
            else
            {
                if(progresController.GetComponent<ProgressConroller>().saveFile.CheckForEnemy(name) || progresController.GetComponent<ProgressConroller>().saveFile.CheckForZone(name) || progresController.GetComponent<ProgressConroller>().saveFile.CheckForMask(name))
                {
                    pag.SetActive(true);
                }
                else
                {
                    pag.SetActive(false);
                }
            }
        }
    }
}
