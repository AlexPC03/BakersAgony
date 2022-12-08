using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private Camera camara;
    public bool shake;
    public bool spore;
    private AudioSource aud;
    private Animator anim;
    private playerMovement playerController;
    public bool friendlyFire=true;
    public float speed=1;
    // Start is called before the first frame update
    void Start()
    {
        aud=GetComponent<AudioSource>();
        anim=GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        aud.pitch = Random.Range(0.8f, 1.2f);
        if(shake)
        {
            camara = Camera.main;
            camara.GetComponent<Animator>().SetTrigger("Shake");
        }
        anim.speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("TakeDamage");
            if(spore)
            {
                camara = Camera.main;
                camara.GetComponent<Animator>().SetTrigger("Funged");
            }
        }
        if ((collision.tag == "Enemy" || collision.tag == "Boss" || collision.tag =="SpecialEnemy") && collision.GetComponent<EnemyBasicLifeSystem>()!=null && friendlyFire)
        {
            collision.SendMessage("TakeDamage", 20+10*playerController.sala/2);
        }
    }

    public void Dissapear()
    {
        Destroy(gameObject);
    }
}
