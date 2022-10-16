using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PedestalController : MonoBehaviour
{
    private GameObject thisObject;
    public GameObject[] list;
    public bool inRange=false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(list[Random.Range(0, list.Length)], transform);
    }

    // Update is called once per frame
    void Update()
    {
        thisObject = transform.GetChild(0).gameObject;
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            thisObject.SendMessage("Pick");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
