using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationInstantiatorController : MonoBehaviour
{
    private GameObject player;
    private GameObject obj;
    private int n;
    public GameObject[] EntryObj;
    public GameObject[] SugarObj;
    public GameObject[] OvenObj;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        n = Random.Range(15,25);
        for (int i=0; i<=n; i++)
        {
            Vector3 v = transform.position-new Vector3(Random.Range(-12f, 12f), Random.Range(-12f, 12f), 0);
            if (player.GetComponent<playerMovement>().zona==playerMovement.zone.entrada)
            {
                obj = EntryObj[Random.Range(0, EntryObj.Length)];
            }
            else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.pastelería)
            {
                obj = SugarObj[Random.Range(0, SugarObj.Length)];
            }
            else if (player.GetComponent<playerMovement>().zona == playerMovement.zone.horneadores)
            {
                obj = OvenObj[Random.Range(0, OvenObj.Length)];
            }
            obj=Instantiate(obj,v,new Quaternion (0,0,0,0));
            if(Random.Range(0,2)==0)
            {
                obj.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
