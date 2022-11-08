using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;



public class PedestalController : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    private SpriteRenderer sp;
    private bool dontDestroy = false;
    private Gamepad gamepad;



    public AudioClip[] sounds;
    private ParticleSystem.EmissionModule em;
    private GameObject thisObject;
    public bool destroyOnPick;
    public bool shop;
    public int price;
    public bool deal;
    public dealTypes dealPrice;
    public enum dealTypes
    {
        none,
        health,
        speed,
        damage
    }
    public Sprite[] dealSprites;

    public bool canDissapear;
    public int oddsToNotDestroy;
    public GameObject[] list;
    public bool inRange=false;
    // Start is called before the first frame update
    void Start()
    {

        aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        sp = GetComponent<SpriteRenderer>();
        em = GetComponent<ParticleSystem>().emission;
        em.enabled = false;
        Instantiate(list[Random.Range(0, list.Length)], transform);
        if(deal)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    dealPrice = dealTypes.health;
                    sp.sprite = dealSprites[0];
                    break;
                case 1:
                    dealPrice = dealTypes.damage;
                    sp.sprite = dealSprites[1];
                    break;
                case 2:
                    dealPrice = dealTypes.speed;
                    sp.sprite = dealSprites[2];
                    break;
            }
        }
        if ((shop ||deal) && sp != null)
        {
            sp.enabled = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        gamepad = Gamepad.current;
        if (canDissapear && player.GetComponent<playerMovement>().sala > 27)
        {
            if (Random.Range(0, oddsToNotDestroy+1) == 0 && dontDestroy==false)
            {
                Destroy(gameObject);
            }
            else
            {
                dontDestroy = true;
            }
        }
        if (!shop && !deal && sp != null)
        {
            sp.enabled = false;
        }
        if (transform.GetChild(0) != null)
        {
            thisObject = transform.GetChild(0).gameObject;
            if (gamepad == null)
            {
                if (Input.GetKeyDown(KeyCode.E) && inRange)
                {
                    if (!shop && !deal)
                    {
                        thisObject.SendMessage("Pick");
                    }
                    else if(shop && !deal)
                    {
                        if (player.GetComponent<playerMovement>().corn >= price)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("loseCorn", price);
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                    }
                    else
                    {
                        if(dealPrice==dealTypes.health)
                        {
                            if (aud != null)
                            {
                                aud.pitch = Random.Range(1.2f, 1.8f);
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseMaxHealth");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;

                        }
                        else if (dealPrice == dealTypes.damage)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseAttack");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                        else if (dealPrice == dealTypes.speed)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseVelocity");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                    }
                    if (destroyOnPick)
                    {
                        Destroy(gameObject, 0.1f);
                    }
                }
            }
            else
            {
                if ((gamepad.aButton.wasPressedThisFrame || gamepad.bButton.wasPressedThisFrame || gamepad.crossButton.wasPressedThisFrame) && inRange)
                {
                    if (!shop && !deal)
                    {
                        thisObject.SendMessage("Pick");
                    }
                    else if (shop && !deal)
                    {
                        if (player.GetComponent<playerMovement>().corn >= price)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("loseCorn", price);
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                    }
                    else
                    {
                        if (dealPrice == dealTypes.health)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseMaxHealth");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;

                        }
                        else if (dealPrice == dealTypes.damage)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseAttack");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                        else if (dealPrice == dealTypes.speed)
                        {
                            if (aud != null)
                            {
                                aud.clip = sounds[Random.Range(0, sounds.Length)];
                                aud.Play();
                            }
                            player.SendMessage("decreaseVelocity");
                            thisObject.SendMessage("Pick");
                            shop = false;
                            deal = false;
                        }
                    }
                    if (destroyOnPick)
                    {
                        Destroy(gameObject, 0.1f);
                    }
                }
            } 
            if(thisObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                em.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
            if (transform.GetChild(0) != null && transform.GetChild(0).GetComponent<SpriteRenderer>().enabled==true)
            {
                if (!shop)
                {
                    em.enabled = true;
                }
                else
                {
                    if (player.GetComponent<playerMovement>().corn >= price)
                    {
                        em.enabled = true;
                    }
                }

            }     
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
            em.enabled = false;
        }
    }
}
