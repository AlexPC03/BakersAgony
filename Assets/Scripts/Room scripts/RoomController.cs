using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool started=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && started == true)
        {
            foreach (GameObject door in transform.GetComponentsInChildren<GameObject>())
            {
                if (door.GetComponent<DoorControler>() != null)
                {
                    door.SendMessage("Unlock");
                }
            }
        }
    }

    public void RoomStart()
    {
        started = true;

        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (door.GetComponent<EnemySpawner>() != null)
            {
                door.SendMessage("StartRoom");
            }
        }
        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (spawner.GetComponent<EnemySpawner>() != null)
            {
                spawner.SendMessage("StartRoom");
            }
        }
    }
}
