using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplphaBunController : BossController
{
    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;
    private List<Vector3> storedPositions;
    private Vector3 targetPos;
    private float timeto=0;


    [Header("Linking atributes")]
    public bool isHead;
    public GameObject headTransform;
    public float distanceFromHead;

    [Header("Atributes")]
    public float range;
    public float nearRange;
    public float speed;
    public float nearSpeed;
    public float targetChangeTime;

    [Header("Information")]
    public float actualDistance;
    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        storedPositions = new List<Vector3>();
        if (headTransform == null)
        {
            anim = GetComponent<Animator>();
        }
        targetPos = transform.position+new Vector3(0,3,0);
    }


    // Update is called once per frame
    void Update()
    {
        CheckOrientation();
        timeto += Time.deltaTime;
        if (headTransform == null)
        {
            anim.SetFloat("Yvelocity", rb.velocity.y); ;
            actualDistance = (player.transform.position - transform.position).magnitude;
            if (actualDistance < range)
            {
                if (actualDistance > nearRange)
                {
                    rb.AddForce((targetPos - transform.position).normalized * speed,ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce((targetPos - transform.position).normalized * nearSpeed, ForceMode2D.Force);
                }
            }
        }
        else
        {
            if (storedPositions.Count == 0)
            {
                storedPositions.Add(headTransform.transform.position); //store the target currect position
                return;
            }
            else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
            {
                storedPositions.Add(headTransform.transform.position); //store the position every frame
            }
            if (storedPositions.Count > distanceFromHead)
            {
                transform.position = storedPositions[0]; //move
                storedPositions.RemoveAt(0); //delete the position that target just move to
            }
        }
        if (timeto > targetChangeTime)
        {
            targetPos = player.transform.position - new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            targetChangeTime = Random.Range(3,7);
        }
        if(!isHead&&headTransform==null)
        {
            TakeDamage(maxVida);
        }
    }


    private void CheckOrientation()
    {
        if (rb.velocity.x > -0.01)
        {
            sp.flipX = false;
        }
        else if (rb.velocity.x < 0.01)
        {
            sp.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * speed / 2, ForceMode2D.Impulse);
            targetPos = player.transform.position - new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        }
    }
}
