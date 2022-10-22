using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    private GameObject player;
    private bool enemies;
    private new AudioSource audio;
    public AudioClip Ambient;
    public AudioClip Battle;
    public AudioClip Shop;
    public bool started=false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        enemies = GameObject.FindWithTag("Enemy") != null;
        if(player.GetComponent<playerMovement>().sala % 8 == 0)
        {
            ChangeClip(Shop);
        }
        else if (enemies)
        {
            ChangeClip(Battle);
        }
        else 
        {
            ChangeClip(Ambient);
        }
        if (!audio.isPlaying)
        {
            PlayOnce();
        }
        else
        {
            started = false;
        }
    }

    public void ChangeClip(AudioClip aclip)
    {
        audio.clip = aclip;
    }

    private void PlayOnce()
    {
        if(!started)
        {
            audio.Play();
            started = true;
        }
    }


}
