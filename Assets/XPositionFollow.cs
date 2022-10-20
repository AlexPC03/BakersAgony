using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPositionFollow : MonoBehaviour
{
    private GameObject follow;
    public float smoothTime;

    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

    }
}
