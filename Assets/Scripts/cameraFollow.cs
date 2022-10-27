using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject follow;
    public float smoothTime;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Boss").Length>0)
        {
            Camera.main.orthographicSize = 6.5f;
        }
        else
        {
            Camera.main.orthographicSize = 5.5f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(posX,posY,transform.position.z);
    }
}
