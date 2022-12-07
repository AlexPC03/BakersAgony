using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutCollisionController : MonoBehaviour
{
    private Camera _camera;
    private float time=1;
    public bool rosco=false;
    public GameObject parent;
    public Orientation orientation;
    public enum Orientation
    {
        down,
        side
    }
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(parent!=null && !collision.isTrigger && time>1)
        {
            if(rosco && collision.gameObject.layer==6)
            {
                _camera.GetComponent<Animator>().SetTrigger("Shake");
            }
            switch(orientation)
            {
                case Orientation.down:
                    parent.SendMessage("DownCollision");
                    break;
                case Orientation.side:
                    parent.SendMessage("SideCollision");
                    break;
            }
            time = 0;
        }
    }
}
