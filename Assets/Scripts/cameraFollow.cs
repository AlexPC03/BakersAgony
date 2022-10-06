using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private GameObject follow;
    public float smoothTime;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.Find("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(posX,posY,transform.position.z);
    }
}
