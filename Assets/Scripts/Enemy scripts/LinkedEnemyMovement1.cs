using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedEnemyMovement1 : EnemyBasicLifeSystem
{
    private Rigidbody2D rb;
    private GameObject player;
    private SpriteRenderer sp;
    private Animator anim;
    private List<Vector3> storedPositions;


    [Header("Linking atributes")]
    public GameObject headTransform;
    public float distanceFromHead;

    [Header("Atributes")]
    public float range;
    public float speed;
    public bool fromVertical;

    [Header("Information")]
    public float actualDistance;
    public float xDistance, yDistance;
    // Start is called before the first frame update
    void Start()
    {
        StartVida();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        storedPositions = new List<Vector3>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (headTransform==null)
        {
            if(Random.Range(0,2)==0)
            {
                fromVertical = true;
            }
            else
            {
                fromVertical = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetBool("head", headTransform == null);
        anim.SetFloat("Yvelocity", rb.velocity.y);
        CheckOrientation();
        if (headTransform == null)
        {
            actualDistance = (player.transform.position - transform.position).magnitude;
            xDistance = player.transform.position.x - transform.position.x;
            yDistance = player.transform.position.y - transform.position.y;

            if (actualDistance < range)
            {
                if(fromVertical)
                {
                    MoveFromVertical();
                }
                else
                {
                    MoveFromHorizontal();
                }
            }

            if(actualDistance < 2.5 && actualDistance > 2.4)
            {
                fromVertical = !fromVertical;
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
    }

    private void MoveFromVertical()
    {

        if (Mathf.Abs(xDistance) >0.2)
        {
            if (xDistance < 0.1)
            {
                rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            }
            else if(xDistance > -0.1)
            {
                rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
        else if(Mathf.Abs(xDistance)<0.2)
        {
            if (yDistance < 0.1)
            {
                rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
            }
            else if (yDistance > -0.1)
            {
                rb.AddForce(Vector2.up * speed, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
        }
    }

    private void MoveFromHorizontal()
    {
        if (Mathf.Abs(yDistance) > 0.2)
        {
            if (yDistance < 0.1)
            {
                rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
            }
            else if (yDistance > -0.1)
            {
                rb.AddForce(Vector2.up * speed, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
        }
        else if (Mathf.Abs(yDistance) < 0.2)
        {
            if (xDistance < 0.1)
            {
                rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            }
            else if (xDistance > -0.1)
            {
                rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
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
}
