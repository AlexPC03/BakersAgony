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
    public Text attackText;
    public Text speedText;
    public Text invulneravilityText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerM.attack * playerM.attackMultiplier==1)
        {
            attackText.text = (playerM.attack * playerM.attackMultiplier).ToString() + ".0";
        }
        else
        {
            attackText.text = (playerM.attack * playerM.attackMultiplier).ToString();
        }
        if (playerM.attack * playerM.attackMultiplier == 1)
        {
            speedText.text = (playerM.runSpeed * playerM.speedMultiplier / 5).ToString() + ".0";
        }
        else
        {
            speedText.text = (playerM.runSpeed * playerM.speedMultiplier / 5).ToString();
        }
        if (playerM.attack * playerM.attackMultiplier == 1)
        {
            invulneravilityText.text = (playerM.invulneravilityTime / 5).ToString() + ".0";
        }
        else
        {
            invulneravilityText.text = (playerM.invulneravilityTime / 5).ToString();
        }
        if (playerM.maxCorn<=99)
        {
            if(playerM.corn<=9)
            {
                cornText.text = "0"+playerM.corn.ToString();
            }
            else
            {
                cornText.text =playerM.corn.ToString();
            }
        }
        else
        {
            if (playerM.corn <= 9)
            {
                cornText.text = "00" + playerM.corn.ToString();
            }
            else if(playerM.corn <= 99)
            {
                cornText.text = "0" + playerM.corn.ToString();
            }
            else
            {
                cornText.text = playerM.corn.ToString();
            }
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
