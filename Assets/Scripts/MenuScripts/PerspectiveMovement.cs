using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveMovement : MonoBehaviour
{
    private Vector3 InitialPosition;
    private Vector2 MousePosition;
    public float maxVariation;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Input.mousePosition;

        transform.position = new Vector3(InitialPosition.x + ((MousePosition.x- Screen.width/2) *(maxVariation*0.001f)), InitialPosition.y, InitialPosition.z);
    }
}
