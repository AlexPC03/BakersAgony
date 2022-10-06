using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    public float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;

    [Header("LifeParameters")]
    public int MaxLife;
    public int health;
    public float invulneravilityTime;
    private float PassedTime;

    public bool invulnerable;

    void Start()
    {
        PassedTime = 0;
        health = MaxLife;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PassedTime <= invulneravilityTime)
        {
            PassedTime += Time.deltaTime;
        }
        if(health<0)
        {
            health = 0;
        }
        if(health>MaxLife)
        {
            health = MaxLife;
        }
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
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

        health -= 1;
        PassedTime = 0;
    }
}
