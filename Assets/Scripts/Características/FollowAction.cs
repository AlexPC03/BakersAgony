using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAction : MonoBehaviour
{
    public GameObject target;
    public float distanceFromTarget;
    private List<Vector3> storedPositions;
    // Start is called before the first frame update
    void Start()
    {
        storedPositions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            if (storedPositions.Count == 0)
            {
                storedPositions.Add(target.transform.position); //store the target currect position
                return;
            }
            else if (storedPositions[storedPositions.Count - 1] != target.transform.position)
            {
                storedPositions.Add(target.transform.position); //store the position every frame
            }
            if (storedPositions.Count > distanceFromTarget)
            {
                transform.position = storedPositions[0]; //move
                storedPositions.RemoveAt(0); //delete the position that target just move to
            }
        }
    }
}
