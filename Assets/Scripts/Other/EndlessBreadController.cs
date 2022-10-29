using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBreadController : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    private Animator anim;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        anim=GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Active", active);
    }

    public void Pick()
    {
        if(!active)
        {
            aud.Play();
            player.GetComponent<playerMovement>().endless = true;
            active = true;
        }
        else
        {
            player.GetComponent<playerMovement>().endless = false;
            active = false;
        }
    }
}