using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public int value;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, Random.Range(-90f,90));

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce((player.transform.position - transform.position).normalized * 5, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==player)
        {
            player.SendMessage("addCorn",value);
            Destroy(gameObject);
        }

    }
}
