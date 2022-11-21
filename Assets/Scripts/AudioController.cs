using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    private GameObject player;
    private int playerSala;
    private bool coroutineStarted=false;
    private bool enemies;
    public bool boss;
    public bool isGhost;
    private bool shopDuck;
    private new AudioSource audio;
    public AudioClip Ambient;
    public AudioClip Battle;
    public AudioClip Shop; 
    public AudioClip Ghost;
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
        playerSala = player.GetComponent<playerMovement>().sala;
        enemies = GameObject.FindWithTag("Enemy") != null;
        boss = GameObject.FindWithTag("Boss") != null;
        shopDuck = GameObject.FindWithTag("Duck") != null;

        if (boss)
        {           
            if (isGhost)
            {
                audio.volume = 0.75f;
                audio.pitch = 1f;
                ChangeClip(Ghost);
                StopCoroutine(playEngineSound());
                coroutineStarted = false;
            }   
            else if ((playerSala - 1)%25==0 && playerSala!=0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1f;
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(playEngineSound());
                }
            }
            else if ((playerSala-1) %17==0 && playerSala != 0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1.05f;
                ChangeClip(Boss2);
                StopCoroutine(playEngineSound());
                coroutineStarted = false;
            }
            else if ((playerSala-1) % 9 == 0 && playerSala != 0)
            {
                audio.volume = 0.2f;
                audio.pitch = 1f;
                ChangeClip(Boss1);
                StopCoroutine(playEngineSound());
                coroutineStarted = false;
            }

        }
        else if (playerSala> 8 && (playerSala-1) % 8 == 0 && shopDuck && !boss &&!isGhost)
        {
            audio.volume = 0.5f;

            ChangeClip(Shop);
        }
        else if (enemies && !boss && !isGhost)
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

    private void FixedUpdate()
    {
        isGhost = GameObject.Find("CrackedGhost(Clone)") || GameObject.Find("CrackedGhostHappy(Clone)") || GameObject.Find("CrackedGhostJelaous(Clone)") || GameObject.Find("CrackedGhostSad(Clone)") || GameObject.Find("CrackedGhost") || GameObject.Find("CrackedGhostHappy") || GameObject.Find("CrackedGhostJelaous") || GameObject.Find("CrackedGhostSad");
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
