using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sporabodyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SlamSpawn()
    {
        transform.parent.SendMessage("SlamSpawn");
    }
    public void SmileSpawn()
    {
        transform.parent.SendMessage("SmileSpawn");
    }

    public void Shake()
    {
        transform.parent.SendMessage("Shake");
    }

    public void StartIdle()
    {
        transform.parent.SendMessage("StartIdle");
    }

    public void StartSpit()
    {
        transform.parent.SendMessage("StartSpit");

    }
}

