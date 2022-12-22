using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookButtonController : MonoBehaviour
{
    private GameObject progresController;
    private Image img;
    private Color initColor;
    public string code;

    // Start is called before the first frame update
    void Start()
    {
        progresController = GameObject.Find("ProgressControl");
        if(transform.childCount>0)
        {
            img = transform.GetChild(0).GetComponent<Image>();
        }
        
        if (img != null)
        {
            initColor = img.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(img != null)
        {
            if(progresController.GetComponent<ProgressConroller>().saveFile.CheckForEnemy(code) || progresController.GetComponent<ProgressConroller>().saveFile.CheckForZone(code) || progresController.GetComponent<ProgressConroller>().saveFile.CheckForMask(code))
            {
                img.color = initColor;

            }
            else
            {
                img.color = Color.clear;
            }
        }
    }
}
