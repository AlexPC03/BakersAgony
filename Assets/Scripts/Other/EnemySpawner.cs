using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;

    private SpriteRenderer sp;

    public bool aditional;

    public bool boss;

    public GameObject[] bossList;
    public GameObject[] enemyListEntry;
    public GameObject[] enemyListSugar;
    public GameObject[] enemyListOven;
    public GameObject enemyToSpawn;
    public bool byDistance;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {

         player = GameObject.FindGameObjectWithTag("Player");


        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;

        if(boss)
        {
            enemyToSpawn = bossList[Random.Range(0, bossList.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona==playerMovement.zone.entrada)
        {
            enemyToSpawn = enemyListEntry[Random.Range(0, enemyListEntry.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pastelería)
        {
            enemyToSpawn = enemyListSugar[Random.Range(0, enemyListSugar.Length)];
        }
        else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
        {
            enemyToSpawn = enemyListOven[Random.Range(0, enemyListOven.Length)];
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
