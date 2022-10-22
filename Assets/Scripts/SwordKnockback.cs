using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnockback : MonoBehaviour
{
    protected GameObject Player;
    protected GameObject SwordBase;
    protected GameObject SwordObj;
    public Sprite InventorySword;

    public float Swordspeed;
    public float neededSpeed;
    public float damage;
    public float forceHit;
    public float rotationForce;
    public float deceleration;
    // Start is called before the first frame update
    protected void StartSword()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SwordBase = GameObject.Find("Sword");
        SwordObj = GameObject.Find("SwordObject");
    }

    // Update is called once per frame
    protected void CheckSpeed()
    {
        Swordspeed = SwordBase.GetComponent<rotateSword>().angVelocity;
    }

    private void FixedUpdate()
    {
        
    }

}
