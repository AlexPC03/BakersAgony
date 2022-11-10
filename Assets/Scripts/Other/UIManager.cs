using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private GameObject player;
    private playerMovement playerM;
    public Text cornText;
    public Text roomText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerM.corn<=9)
        {
            cornText.text = "0"+playerM.corn.ToString();
        }
        else
        {
            cornText.text =playerM.corn.ToString();
        }
        if(playerM.endless)
        {
            roomText.text = playerM.sala.ToString();
        }
        else
        {
            roomText.text = "";
        }
    }
}
