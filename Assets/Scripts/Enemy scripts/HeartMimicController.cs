using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMimicController : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;
    private Vector3 space;
    private bool awake = false;
    public bool startAwake = false;
    public float range;
    public float rotationRange;
    public float speed;
    public GameObject proyectile;
    public int nProy;

    // Start is called before the first frame update
    void Start()
    {

        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (startAwake)
        {
            anim.SetTrigger("WakeUp");
        }
        space = new Vector3(rotationRange, 0, 0);
        if (maxVida <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((player.transform.position + new Vector3(0, -0.3f, 0) - transform.position).magnitude < range && !awake)
        {
            anim.SetTrigger("WakeUp");
        }
        if (awake)
        {
            rb.AddForce((player.transform.position + new Vector3(0, -0.3f, 0) + space - transform.position).normalized * speed, ForceMode2D.Force);
        }

    }

    public void StartOrbiting()
    {
        awake = true;
        space = Rotate(space, Random.Range(-180f, 180f));
    }
    public void Shoot()
    {
        space = Rotate(space, Random.Range(-180f, 180f));
        for (int i = 0; i < nProy; i++)
        {
            GameObject proy = Instantiate(proyectile, transform.position, new Quaternion(0, 0, 0, 0));
            if (proy.GetComponent<BreadMageProyectileMovement>() != null)
            {
                proy.GetComponent<BreadMageProyectileMovement>().target = player;
                proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-i, i), Random.Range(-i, i), 0);
            }
            else if (proy.GetComponent<BumeranProyectileMovement>() != null)
            {
                proy.GetComponent<BumeranProyectileMovement>().target = player;
                proy.GetComponent<BumeranProyectileMovement>().targetPos = player.transform.position + new Vector3(Random.Range(-i, i), Random.Range(-i, i), 0);
            }
        }
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void Pick()
    {
    }
}

