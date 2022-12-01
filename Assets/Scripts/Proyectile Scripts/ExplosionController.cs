using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private Camera camara;
    public bool shake;
    private AudioSource aud;
    private playerMovement playerController;
    public bool friendlyFire=true;
    // Start is called before the first frame update
    void Start()
    {
        aud=GetComponent<AudioSource>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        aud.pitch = Random.Range(0.8f, 1.2f);
        if(shake)
        {
            camara = Camera.main;
            camara.GetComponent<Animator>().SetTrigger("Shake");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("TakeDamage");
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
