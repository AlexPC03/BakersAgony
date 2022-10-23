using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBreadController : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;
    public bool used = false;
    public Type tipo;
    public enum Type
    {
        Health,
        Speed,
        Atack
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void Pick()
    {
        if (!used)
        {
            if(aud!=null)
            {
                aud.Play();
            }
            if (tipo == Type.Health)
            {
                player.SendMessage("increaseMaxHealth");
                player.SendMessage("RecoverLife");
                player.SendMessage("RecoverLife");
            }
            else if (tipo == Type.Speed)
            {
                player.SendMessage("increaseVelocity");
            }
            else if (tipo == Type.Atack)
            {
                player.SendMessage("increaseAttack");
            }
        }

        used = true;
    }
}
