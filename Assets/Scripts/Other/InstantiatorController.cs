using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorController : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public GameObject objectVariantToInstantiate;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (objectVariantToInstantiate != null && Random.Range(0f,100f)< player.GetComponent<playerMovement>().sala && Random.Range(0, 3)==0)
        {
            Instantiate(objectVariantToInstantiate, transform.position, new Quaternion(0, 0, 0, 0));
        }
        else if(objectToInstantiate!=null)
        {
            Instantiate(objectToInstantiate, transform.position, new Quaternion(0, 0, 0,0));
        }

        Destroy(gameObject);
    }

}
