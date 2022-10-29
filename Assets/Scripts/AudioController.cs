using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    private GameObject player;
    private bool coroutineStarted=false;
    private bool enemies;
    private bool boss;
    private new AudioSource audio;
    public AudioClip Ambient;
    public AudioClip Battle;
    public AudioClip Shop;
    public AudioClip Boss1;
    public AudioClip Boss2;
    public AudioClip Boss3Intro;
    public AudioClip Boss3Bucle;
    public bool started = false;
    public AudioClip actual;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        actual = audio.clip;
        enemies = GameObject.FindWithTag("Enemy") != null;
        boss = GameObject.FindWithTag("Boss") != null;
        if (boss)
        {      
            if ((player.GetComponent<playerMovement>().sala - 1)%25==0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1f;
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(playEngineSound());
                }
            }
            else if ((player.GetComponent<playerMovement>().sala-1) %17==0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1.05f;
                ChangeClip(Boss2);
                StopCoroutine(playEngineSound());
                coroutineStarted = false;

            }
            else if ((player.GetComponent<playerMovement>().sala-1) % 9 == 0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1f;
                ChangeClip(Boss1);
                StopCoroutine(playEngineSound());
                coroutineStarted = false;
            }


        }
        else if (player.GetComponent<playerMovement>().sala> 8 && (player.GetComponent<playerMovement>().sala-1) % 8 == 0)
        {
            audio.volume = 0.5f;

            ChangeClip(Shop);
        }
        else if (enemies)
        {
            audio.volume = 0.5f;
            audio.pitch = 1f;
            ChangeClip(Battle);
        }
        else
        {
            audio.volume = 0.6f;
            audio.pitch = 1f;
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
        if (!started)
        {
            audio.Play();
            started = true;
        }
    }

    IEnumerator playEngineSound()
    {
        audio.clip = Boss3Intro;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = Boss3Bucle;
        audio.Play();
    }
}
