using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private SpriteRenderer sp;


    public GameObject[] enemyList;
    public GameObject enemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
        enemyToSpawn = enemyList[Random.Range(0, enemyList.Length)];
        StartRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

    }

    public void StartRoom()
    {
        Instantiate(enemyToSpawn,transform.position,new Quaternion(0,0,0,0));
    }
}
