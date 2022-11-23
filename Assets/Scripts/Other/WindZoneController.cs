using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    private GameObject player;
    private ParticleSystem part;
    private CircleCollider2D rb;
    private float distanceToDestroy;
    public bool pointWind;
    public bool inverted;
    public float windForce;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        part=GetComponent<ParticleSystem>();
        rb = GetComponent<CircleCollider2D>();
        distanceToDestroy = (float)(rb.radius+0.5);
    }

    // Update is called once per frame
    void Update()
    {
        var main = part.main;
        var emi = part.emission;

        main.startSpeed = 10 * windForce;
        emi.rateOverTime = 15 * Mathf.Abs(windForce);

        if(pointWind && windForce>0)
        {
            ParticleSystem.Particle[] ps = new ParticleSystem.Particle[part.particleCount];
            part.GetParticles(ps);
            // keep only particles that are within DistanceToDestroy
            var distanceParticles = ps.Where(p => Vector3.Distance(transform.position, p.position) < distanceToDestroy).ToArray();
            part.SetParticles(distanceParticles, distanceParticles.Length);
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
}
