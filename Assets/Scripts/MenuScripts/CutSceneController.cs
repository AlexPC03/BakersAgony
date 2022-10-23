using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneController : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            anim.speed = 15;
        }
        else
        {
            anim.speed = 1;

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
