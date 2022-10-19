using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public ParticleSystem par;
    private Rigidbody2D body;
    private SpriteRenderer sp;
    public GameObject healthbar;
    public GameObject maxHealthbar;
    public GameObject playerBody;
    public GameObject lightSpot;
    public GameObject lightWorld;
    public int sala;

    public float attack = 1f;

    private float horizontal;
    private float vertical;

    public Color lerpedColor = Color.white;


    public float moveLimiter = 0.7f;
    public float runSpeed;

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
        maxMaxLife = 12;
        PassedTime = 0;
        health = maxLife-1;
        body = GetComponent<Rigidbody2D>();
        sp = playerBody.GetComponent<SpriteRenderer>();
        invulnerable = false;

    }

    void Update()
    {
        sp.color = lerpedColor;
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
        vertical = Input.GetAxisRaw("Vertical"); // -1 es derecha

        if(horizontal>0)
        {
            playerBody.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(horizontal<0)
        {
            playerBody.GetComponent<SpriteRenderer>().flipX = false;
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
        }
        else
        {
            lerpedColor = Color.white;
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // movimiento diagonal
        {
            // limitar velocidad diagonal
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        if (PassedTime >= invulneravilityTime)
        {
            invulnerable = false;
        }
        else
        { 
            invulnerable = true;
        }


    }

    public void TakeDamage()
    {
        if(!invulnerable)
        {
            health -= 1;
            PassedTime = 0;
            par.Play();
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
        maxLife += 2;
    }
    public void increaseVelocity()
    {
        runSpeed += 1.5f;
    }
    public void increaseAttack()
    {
        attack += 0.2f;
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
}
