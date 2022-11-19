using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : EnemyBasicLifeSystem
{
    private float time = 0;
    private int actualRoom;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private GameObject player;
    public Sprite[] sprites;
    public Type type;
    public Transform point;
    private Vector3 dir;
    public float speed;
    public enum Type
    {
        Seeker,
        Liner,
        Rotater
    }
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponentInChildren<SpriteRenderer>();
        spr.sprite = sprites[Random.Range(0, sprites.Length)];
        player = GameObject.FindGameObjectWithTag("Player");
        actualRoom = player.GetComponent<playerMovement>().sala;
        if(type==Type.Liner && point != null)
        {
            dir = (point.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time<=1)
        {
            time += Time.deltaTime*3;
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(1, 1, 1),time);
        }
        switch (type)
        {
            case Type.Seeker:
                if (player != null)
                {
                    rb.velocity = (player.transform.position - transform.position).normalized * speed;
                }
                break;
            case Type.Liner:
                if (point != null)
                {
                    rb.velocity = dir * speed;
                }
                else if(point == null ||(point.position- transform.position).magnitude>60)
                {
                    vida = 0;
                }
                break;
            case Type.Rotater:
                if (point != null)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    var q = transform.rotation;
                    transform.RotateAround(point.position, Vector3.forward, 20 * Time.deltaTime * speed);
                    transform.rotation = q;
                    if(player.transform.position.x-transform.position.x>0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    vida = 0;
                }
                break;
        }
        if (player.GetComponent<playerMovement>().sala != actualRoom)
        {
            vida = 0;
        }
        if (time > 1)
        {
            CheckOrientation();
        }
    }

    private void CheckOrientation()
    {
        if (rb.velocity.x < -0.01)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x > 0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

