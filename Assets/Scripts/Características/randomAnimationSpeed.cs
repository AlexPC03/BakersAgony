using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomAnimationSpeed : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        if(anim!=null)
        {
            anim.speed = Random.Range(0.5f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
