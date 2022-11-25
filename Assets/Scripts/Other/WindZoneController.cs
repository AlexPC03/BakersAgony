using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    private float initRadius;
    private bool disappearing=false;
    private float time = 0;
    public bool attached=false;
    public GameObject follow;
    private GameObject player;
    private ParticleSystem part;
    private CircleCollider2D circCol;
    private float distanceToDestroy;
    public bool pointWind;
    public bool startInitiated=true;
    public bool inverted;
    public float windForce;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        part=GetComponent<ParticleSystem>();
        if(pointWind)
        {
            circCol = GetComponent<CircleCollider2D>();
            initRadius = circCol.radius;
            if(startInitiated)
            {
                Appear();
            }
            else
            {
                Disappear();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(pointWind)
        {
            distanceToDestroy = (float)(circCol.radius + 0.5);
        }

        time += Time.deltaTime;
        var main = part.main;
        var emi = part.emission;
        var circ = part.shape;

        main.startSpeed = 10 * windForce;
        emi.rateOverTime = 15 * Mathf.Abs(windForce);
        if (pointWind && windForce < 0)
        {
            circ.radius = (float)(this.circCol.radius + 0.1);
        }
        if (pointWind && windForce>0)
        {
            ParticleSystem.Particle[] ps = new ParticleSystem.Particle[part.particleCount];
            part.GetParticles(ps);
            // keep only particles that are within DistanceToDestroy
            var distanceParticles = ps.Where(p => Vector3.Distance(transform.position, p.position) < distanceToDestroy).ToArray();
            part.SetParticles(distanceParticles, distanceParticles.Length);
        }

        if(attached)
        {
            if(follow!=null)
            {
                transform.position=follow.transform.position;
            }
            else
            {
                player.GetComponent<playerMovement>().speedModifierVector = Vector2.zero;
                Destroy(gameObject);
            }
        }

        if (!disappearing)
        {
            circCol.radius = Mathf.Lerp(0, initRadius, time);
        }
        else
        {
            circCol.radius = Mathf.Lerp(initRadius, 0, time);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject==player)
        {
            if(!pointWind)
            {
                player.GetComponent<playerMovement>().speedModifierVector=gameObject.transform.up*windForce;
            }
            else
            {
                if(inverted)
                {
                    player.GetComponent<playerMovement>().speedModifierVector = (transform.position - player.transform.position).normalized * -windForce;
                }
                else
                {
                    player.GetComponent<playerMovement>().speedModifierVector = (transform.position - player.transform.position).normalized * windForce;
                }
            }
        }
        else if(collision.tag=="Enemy" || collision.tag == "SpecialEnemy")
        {
            if (!pointWind)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * windForce*0.75f,ForceMode2D.Force);
            }
            else
            {
                if (inverted)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position).normalized * -windForce * 0.75f, ForceMode2D.Force);
                }
                else
                {
                    collision.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position).normalized * windForce * 0.75f, ForceMode2D.Force);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<playerMovement>().speedModifierVector = Vector2.zero;
        }
    }

    public void Appear()
    {
        disappearing = false;
        time = 0;
    }

    public void Disappear()
    {
        disappearing=true;
        time = 0;
    }
}
