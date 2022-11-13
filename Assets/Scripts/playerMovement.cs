using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    private Gamepad gamepad;
    private GameObject ratTarget;


    private GameObject shield;

    public AudioClip takeDamage;
    public AudioClip die;
    public ParticleSystem par;
    private AudioSource aud;
    private Rigidbody2D body;
    private SpriteRenderer sp;
    public GameObject healthbar;
    public GameObject maxHealthbar;
    public GameObject playerBody;
    public GameObject maskObject;
    public GameObject lightSpot;
    public GameObject lightWorld;
    private bool doctorMask=false;
    private bool mouseMask = false;
    public GameObject[] spludges;
    public GameObject[] Hearts;
    public Sprite halfHeart;
    public Sprite normalHeart;
    public Sprite cheeseHeart;
    public int sala;
    public bool endless=false;

    public float maxAtack = 2f;
    public float attack = 1f;
    public float attackMultiplier = 1;

    public int maxCorn = 99;
    public float maxVelocity=10f;
    private float horizontal;
    private float vertical;

    public Color lerpedColor = Color.white;
    public Color lerpedColorM = new Color (0.7264151f, 0.7264151f, 0.7264151f,1);


    public float moveLimiter = 0.7f;
    public float runSpeed;
    public float speedMultiplier=1;

    public int corn = 0;

    [Header("LifeParameters")]
    private int maxMaxLife;
    public int maxLife;
    public int health;
    public float invulneravilityTime;
    private float PassedTime;

    public bool invulnerable;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6);

        shield = GameObject.Find("RotationPointShield");

        maxMaxLife = 12;
        PassedTime = 0;
        health = maxLife-1;
        aud = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        sp = playerBody.GetComponent<SpriteRenderer>();
        invulnerable = false;

    }

    void Update()
    {
        ratTarget = GameObject.Find("RatTarget");

        gamepad = Gamepad.current;
        //Cheat
        if(gamepad==null)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.K) && !endless)
            {
                RecoverLife();
            }
        }
        else
        {
            if (((gamepad.aButton.isPressed && gamepad.yButton.isPressed && gamepad.xButton.isPressed) || (gamepad.crossButton.isPressed && gamepad.squareButton.isPressed && gamepad.triangleButton.isPressed)) && !endless)
            {
                RecoverLife();
            }
        }


        sp.color = lerpedColor;
        maskObject.GetComponentInChildren<SpriteRenderer>().color = lerpedColorM;
        if (PassedTime <= invulneravilityTime)
        {
            PassedTime += Time.deltaTime;
        }
        if(maxLife<0)
        {
            maxLife = 0;
        }
        if(maxLife>maxMaxLife)
        {
            maxLife = maxMaxLife;
        }
        if(health<0)
        {
            health = 0;
        }
        if(health>maxLife)
        {
            health = maxLife;
        }
        if(health<=0)
        {
            lightWorld.SetActive(false);
            lightSpot.SetActive(false);
            body.velocity = Vector2.zero;
            Invoke("SceneChange", 1.5f);
        }

        healthbar.GetComponent<Animator>().SetInteger("Health", health);
        maxHealthbar.GetComponent<Animator>().SetInteger("MaxHealth", maxLife);

        // valor entre -1 y 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 es izquierda
        vertical = Input.GetAxisRaw("Vertical"); // -1 es abajo

        if(horizontal>0)
        {
            sp.flipX = true;
            maskObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(horizontal<0)
        {
            sp.flipX = false;
            maskObject.transform.localScale = new Vector3(1, 1, 1);
        }


        GetComponent<Animator>().SetFloat("vertical", vertical);

        if(horizontal==0 && vertical==0)
        {
            GetComponent<Animator>().SetBool("moving", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", true);
        }

        if (invulnerable)
        {
            lerpedColor = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time * 10, 1));
            lerpedColorM = Color.Lerp(new Color(0.7264151f, 0.7264151f, 0.7264151f, 1), Color.clear, Mathf.PingPong(Time.time * 10, 1));
        }
        else
        {
            lerpedColor = Color.white;
            lerpedColorM = new Color(0.7264151f, 0.7264151f, 0.7264151f, 1);
        }
        if (mouseMask)
        {
            ratTarget.GetComponent<Light2D>().enabled = true;
            ratTarget.GetComponent<SpriteRenderer>().enabled = true;
            if (health == maxLife)
            {
                switch (maxLife)
                {
                    case 2:
                        Hearts[0].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[1].GetComponent<Image>().sprite = cheeseHeart;
                        break;
                    case 4:
                        Hearts[2].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[3].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[0].GetComponent<Image>().sprite = halfHeart;
                        Hearts[1].GetComponent<Image>().sprite = normalHeart;
                        break;
                    case 6:
                        Hearts[4].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[5].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[1].GetComponent<Image>().sprite = normalHeart;
                        Hearts[0].GetComponent<Image>().sprite = halfHeart;
                        Hearts[2].GetComponent<Image>().sprite = halfHeart;
                        Hearts[3].GetComponent<Image>().sprite = normalHeart;
                        break;
                    case 8:
                        Hearts[6].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[7].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[1].GetComponent<Image>().sprite = normalHeart;
                        Hearts[0].GetComponent<Image>().sprite = halfHeart;
                        Hearts[2].GetComponent<Image>().sprite = halfHeart;
                        Hearts[3].GetComponent<Image>().sprite = normalHeart;
                        Hearts[4].GetComponent<Image>().sprite = halfHeart;
                        Hearts[5].GetComponent<Image>().sprite = normalHeart;
                        break;
                    case 10:
                        Hearts[8].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[9].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[1].GetComponent<Image>().sprite = normalHeart;
                        Hearts[0].GetComponent<Image>().sprite = halfHeart;
                        Hearts[2].GetComponent<Image>().sprite = halfHeart;
                        Hearts[3].GetComponent<Image>().sprite = normalHeart;
                        Hearts[4].GetComponent<Image>().sprite = halfHeart;
                        Hearts[5].GetComponent<Image>().sprite = normalHeart;
                        Hearts[6].GetComponent<Image>().sprite = halfHeart;
                        Hearts[7].GetComponent<Image>().sprite = normalHeart;
                        break;
                    case 12:
                        Hearts[10].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[11].GetComponent<Image>().sprite = cheeseHeart;
                        Hearts[1].GetComponent<Image>().sprite = normalHeart;
                        Hearts[0].GetComponent<Image>().sprite = halfHeart;
                        Hearts[2].GetComponent<Image>().sprite = halfHeart;
                        Hearts[3].GetComponent<Image>().sprite = normalHeart;
                        Hearts[4].GetComponent<Image>().sprite = halfHeart;
                        Hearts[5].GetComponent<Image>().sprite = normalHeart;
                        Hearts[6].GetComponent<Image>().sprite = halfHeart;
                        Hearts[7].GetComponent<Image>().sprite = normalHeart;
                        Hearts[8].GetComponent<Image>().sprite = halfHeart;
                        Hearts[9].GetComponent<Image>().sprite = normalHeart;
                        break;
                }
            }
        }
        else
        {

            ratTarget.GetComponent<Light2D>().enabled=false;
            ratTarget.GetComponent<SpriteRenderer>().enabled=false;

            int i = 0;
            foreach (GameObject obj in Hearts)
            {
                if (obj.GetComponentInParent<Image>().sprite == cheeseHeart)
                {
                    if(i==0 || i%2==0)
                    {
                        obj.GetComponentInParent<Image>().sprite = halfHeart;
                    }
                    else
                    {
                        obj.GetComponentInParent<Image>().sprite = normalHeart;
                    }
                }
                i++;
            }
        }
        CheckStates();
    }

    void FixedUpdate()
    {
        if(gamepad==null)
        {
            if (horizontal != 0 && vertical != 0) // movimiento diagonal
            {
                // limitar velocidad diagonal
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
        }
            body.velocity = new Vector2(horizontal * runSpeed * speedMultiplier, vertical * runSpeed * speedMultiplier);

        if (PassedTime >= invulneravilityTime)
        {
            invulnerable = false;
        }
        else
        { 
            invulnerable = true;
        }

        if (health == 1)
        {
            aud.clip = die;
        }
        else
        {
            aud.clip = takeDamage;
        }

        MaskInteractions();
    }

    public void TakeDamage()
    {
        if(!invulnerable)
        {
            aud.Play();
            if(mouseMask && health==maxLife)
            {
                health -= 2;
            }
            else
            {
                health -= 1;
            }
            PassedTime = 0;
            par.Play();
            if(doctorMask)
            {
                Instantiate(spludges[Random.Range(0, spludges.Length)], transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
    }

    public void RecoverLife()
    {
        health += 1;
    }

    public void addCorn(int points)
    {
        corn += points;
    }

    public void loseCorn(int points)
    {
        corn -= points;
    }

    public void increaseMaxHealth()
    {
        if(maxLife<maxMaxLife)
        {
            maxLife += 2;
        }
        else
        {
            corn += Random.Range(35, 41);
        }
    }
    public void decreaseMaxHealth()
    {
        if (maxLife > 0)
        {
            maxLife -= 2;
        }
    }

    public void increaseVelocity()
    {
        if (runSpeed < maxVelocity)
        {
            runSpeed += 1;
        }
        else
        {
            corn += Random.Range(35, 41);
        }
    }
    public void decreaseVelocity()
    {
        runSpeed -= 1f;
    }
    public void increaseAttack()
    {
        if (attack < maxAtack)
        {        
            attack += 0.2f;
        }
        else
        {
            corn += Random.Range(35, 41);
        }
    }
    public void decreaseAttack()
    {
        attack -= 0.2f;
    }

    public void nextRoom()
    {
        sala++;
        lightWorld.GetComponent<Light2D>().intensity = Random.Range(0.3f, 0.7f);
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void CheckStates()
    {
        if(runSpeed>maxVelocity)
        {
            runSpeed= maxVelocity;
        }
        if(runSpeed<2f)
        {
            runSpeed = 2f;
        }
        if(attack>maxAtack)
        {
            attack= maxAtack;
        }
        if(attack<0.2f)
        {
            attack = 0.2f;
        }
        if(corn>maxCorn)
        {
            corn= maxCorn;
        }
    }

    private void MaskInteractions()
    {
        GameObject mask = maskObject.transform.GetChild(0).gameObject;
        if (mask.tag=="Mask" && mask.GetComponent<MaskController>()!=null)
        {
            int ID = mask.GetComponent<MaskController>().maskID;
            if(ID==1)
            {
                invulneravilityTime = 7.5f;
                attackMultiplier = 0.9f;
            }
            else
            {
                invulneravilityTime = 5;
                attackMultiplier = 1;
            }
            if (ID==2)
            {
                maxCorn = 999;
            }
            else
            {
                maxCorn = 99;
            }
            if(ID==3)
            {
                if(shield.activeSelf==false)
                {
                    shield.SetActive(true);
                }
                speedMultiplier = 0.75f;
            }
            else
            {
                if (shield.activeSelf == true)
                {
                    shield.SetActive(false);
                }
                speedMultiplier = 1f;
            }
            if (ID==4)
            {
                doctorMask = true;
                speedMultiplier = 1.25f;
            }
            else
            {
                doctorMask = false;
                speedMultiplier = 1f;
            }
            if (ID==5)
            {
            }
            else
            {

            }
            if (ID == 6)
            {
                mouseMask = true;
            }
            else
            {
                mouseMask = false;
            }
        }
    }
}
