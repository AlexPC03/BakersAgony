using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer sp;

    public bool aditional;


    public GameObject[] enemyList;
    public GameObject enemyToSpawn;
    public bool byDistance;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        if(byDistance)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
        if(player.GetComponent<playerMovement>().endless)
        {
            enemyToSpawn = enemyList[Random.Range(0, enemyList.Length)];
        }
        else
        {
            enemyToSpawn = enemyList[Random.Range(0, 8)];
        }

        if (!byDistance)
        {
            StartRoom();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (byDistance && (player.transform.position - transform.position).magnitude < distance)
        {
            StartRoom();
            Destroy(gameObject);
        }
    }

    public void StartRoom()
    {
        GameObject enem=Instantiate(enemyToSpawn,transform.position,new Quaternion(0,0,0,0));
        if(aditional)
        {
            enem.AddComponent<DestroyByDistance>();
            enem.tag = "SpecialEnemy";
        }
    }
}
