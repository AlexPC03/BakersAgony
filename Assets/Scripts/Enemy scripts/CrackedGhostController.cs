using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CrackedGhostController : BossController
{
    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;
    private float timeToStart = 0;
    private float timeToAction = 5;
    private float actualTimeForNextAction;
    private float changeTime = 0;
    private bool teleporting;
    public float startTime;
    public GameObject ghostBoom;
    public GameObject seekGhost;
    public GameObject GhostLineR;
    public GameObject GhostLineL;
    [Header("Attack atributes")]
    [Header("Bombarder")]
    public float timeDifference= 0.5f;
    public float spaceRange= 4f;
    public float spaceRangeSimple= 1f;
    public int packQuantity = 3;

    [Header("InvokeSeeker")]
    public float timeDifferenceS = 0.2f;

    [Header("SpawnLine")]
    public float lineProbabilityMin = 0.45f;
    public float lineProbabilityMax = 0.55f;


    [Header("Atributes")]
    public float timeForNextAction;
    public float speed;

    [Header("Information")]
    public float actualDistance;
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        StartBoss();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        actualTimeForNextAction = timeForNextAction;
        target = player.transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckOrientation();
        actualDistance = (player.transform.position - transform.position).magnitude*speed;
        if (timeToStart > startTime)
        {
            rb.velocity = (target-transform.position).normalized*speed;
            timeToAction += Time.deltaTime;
            if(timeToAction>actualTimeForNextAction)
            {
                target = player.transform.position + new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0);
                anim.SetTrigger("Attack");
                timeToAction = 0;
                actualTimeForNextAction = timeForNextAction + (vida / maxVida * timeForNextAction / 2)-Random.Range(-0.1f,0.25f)-player.GetComponent<playerMovement>().sala/200;
            }
            if(actualDistance<2.5 && !teleporting)
            {
                Invoke("Teleport",Random.Range(2f,5f));
            }
            if(teleporting)
            {
                sp.color = Color.Lerp(new Color(sp.color.r, sp.color.g, sp.color.b, 0.75f), new Color(sp.color.r, sp.color.g, sp.color.b,0),changeTime);
                GetComponent<Light2D>().intensity = Mathf.Lerp(0.75f, 0,changeTime);
                if (sp.color.a == 0)
                {
                    float x = Random.Range(3f,6f);
                    float y = Random.Range(3f, 6f);
                    if (Random.Range(0,2)==0)
                    {
                        x = -x;
                    }
                    if (Random.Range(0, 2) == 0)
                    {
                        y = -y;
                    }
                    transform.position = player.transform.position + new Vector3(x, y, 0);
                    teleporting = false;
                    changeTime = 0;
                }
            }
            else
            {
                sp.color = Color.Lerp(new Color(sp.color.r, sp.color.g, sp.color.b, 0), new Color(sp.color.r, sp.color.g, sp.color.b, 0.75f), changeTime);
                GetComponent<Light2D>().intensity = Mathf.Lerp(0, 0.75f, changeTime);
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(1, 1, 1), timeToStart*2);
            timeToStart += Time.deltaTime;
        }
        if(changeTime<=1)
        {
            changeTime += Time.deltaTime;
        }
    }

    public void ChooseAction()
    {
        float choose= Random.Range(0, 1f);
        if (choose < lineProbabilityMin)
        {
            Bombard();
            Invoke("Bombard", Random.Range(0.15f+ timeDifference, 0.5f + timeDifference));
            Invoke("Bombard", Random.Range(0.35f + timeDifference, 0.7f + timeDifference));
            Invoke("Bombard", Random.Range(0.55f + timeDifference, 0.9f + timeDifference));
            if (vida < maxVida / 2)
            {
                Invoke("Bombard", Random.Range(0.75f + timeDifference, 1.1f + timeDifference));
                if (Random.Range(0, 3) == 0)
                {
                    Invoke("Bombard", Random.Range(0.95f + timeDifference, 1.3f + timeDifference));
                }
            }
        }
        else if (choose > lineProbabilityMin && choose < lineProbabilityMax)
        {
            InvokeLine();
            SimpleBombard();
        }
        else if (choose > lineProbabilityMax)
        {
            InvokeSeeker();
            Invoke("InvokeSeeker", Random.Range(timeDifferenceS, timeDifferenceS*2));
            Invoke("InvokeSeeker", Random.Range(timeDifferenceS * 2, timeDifferenceS * 3));
            if (vida < maxVida / 2)
            {
                Invoke("InvokeSeeker", Random.Range(timeDifferenceS * 3, timeDifferenceS * 4));
                Invoke("InvokeSeeker", Random.Range(timeDifferenceS * 4, timeDifferenceS * 5));
            }
        }
    }

    private void CheckOrientation()
    {
        if (player.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void SimpleBombard()
    {
            Instantiate(ghostBoom, player.transform.position + new Vector3(Random.Range(-spaceRangeSimple, spaceRangeSimple), Random.Range(-spaceRangeSimple, spaceRangeSimple), 0), new Quaternion(0, 0, 0, 0));
    }

    private void Bombard()
    {
        int times = Random.Range(packQuantity-1, packQuantity+2);
        for(int i=0;i<times;i++)
        {
            Instantiate(ghostBoom, player.transform.position + new Vector3(Random.Range(-spaceRange, spaceRange), Random.Range(-spaceRange, spaceRange), 0), new Quaternion(0, 0, 0, 0));
        }
    }

    private void InvokeLine()
    {
        if(vida>maxVida/2)
        {
            if(Random.Range(0,2)==0)
            {
                Instantiate(GhostLineL, new Vector3(25,player.transform.position.y+Random.Range(-3,3),0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                Instantiate(GhostLineR, new Vector3(-25, player.transform.position.y + Random.Range(-3, 3), 0), new Quaternion(0, 0, 0, 0));
            }
        }
        else
        {
            Instantiate(GhostLineL, new Vector3(25, player.transform.position.y + Random.Range(-1, 1), 0), new Quaternion(0, 0, 0, 0));
            Instantiate(GhostLineR, new Vector3(-25, player.transform.position.y + Random.Range(-1, 1), 0), new Quaternion(0, 0, 0, 0));
        }
    }

    private void InvokeSeeker()
    {
        Instantiate(seekGhost,transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), new Quaternion(0, 0, 0, 0));
    }

    private void Teleport()
    {
        teleporting = true;
        changeTime = 0;
    }
}
