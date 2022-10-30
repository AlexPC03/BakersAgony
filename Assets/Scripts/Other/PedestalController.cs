using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PedestalController : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    private SpriteRenderer sp;
    private bool dontDestroy = false;

    public AudioClip[] sounds;
    private ParticleSystem.EmissionModule em;
    private GameObject thisObject;
    public bool destroyOnPick;
    public bool shop;
    public int price;
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
        if (shop && sp != null)
        {
            sp.enabled = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (!shop && sp != null)
        {
            sp.enabled = false;
        }
        if (transform.GetChild(0) != null)
        {
            thisObject = transform.GetChild(0).gameObject;

            if (Input.GetKeyDown(KeyCode.E) && inRange)
            {
                if(!shop)
                {
                    thisObject.SendMessage("Pick");
                }
                else
                {
                    if(player.GetComponent<playerMovement>().corn>=price)
                    {
                        if(aud!=null)
                        {
                            aud.clip = sounds[Random.Range(0, sounds.Length)];
                            aud.Play();
                        }
                        player.SendMessage("loseCorn",price);
                        thisObject.SendMessage("Pick");
                        shop = false;
                    }
                }
                if (destroyOnPick)
                {
                    Destroy(gameObject, 0.1f);
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
