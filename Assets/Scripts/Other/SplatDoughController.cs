using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatDoughController : MonoBehaviour
{
    private Color color;
    public GameObject doughHand;
    public Vector2 spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        color=GetComponent<SpriteRenderer>().color;
        if(doughHand != null)
        {
            int i=Random.Range(2, 5);
            for (int j=0;j<i;j++)
            {
                GameObject hand= Instantiate(doughHand, transform.position+new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y),-0.01f),new Quaternion (0,0,0,0),transform);
                if (Random.Range(0,1f)<0.5)
                {
                    hand.transform.localScale = new Vector3(-1, 1, 1);
                }
                hand.GetComponent<Animator>().SetInteger("Hand", Random.Range(1, 4));
                hand.GetComponentInChildren<SpriteRenderer>().color = color;
            }
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
    }
}
