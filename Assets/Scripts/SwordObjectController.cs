using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObjectController : MonoBehaviour
{
    private GameObject Player;
    public GameObject sword;
    private SwordParameters parameters;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(sword.GetComponent<SwordParameters>()!=null)
        {
            parameters = sword.GetComponent<SwordParameters>();
        }
    }
}
