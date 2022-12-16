using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    private GameObject player;
    public GameObject proyectile;
    public float proyectileNumber;
    [Range(0, 1)]
    public float destroyProb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.GetComponent<playerMovement>().skeletonMask && collision.gameObject.tag=="Sword" && collision.GetComponent<SwordParameters>()!=null)
        {
            if(Mathf.Abs(collision.GetComponent<SwordParameters>().Swordspeed)> collision.GetComponent<SwordParameters>().neededSpeed*0.75f)
            {
                if (proyectileNumber == 1){
                    GameObject proy=Instantiate(proyectile,transform.position,new Quaternion (0,0,0,0));
                    if(proy.GetComponent<BreadMageProyectileMovement>()!=null)
                    {
                        proy.GetComponent<BreadMageProyectileMovement>().target = player;
                        proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position;
                    }
                }
                else
                {
                    for(int i=0;i< proyectileNumber;i++)
                    {
                        GameObject proy = Instantiate(proyectile, transform.position, new Quaternion(0, 0, 0, 0));
                        if (proy.GetComponent<BreadMageProyectileMovement>() != null)
                        {
                            proy.GetComponent<BreadMageProyectileMovement>().target = player;
                            proy.GetComponent<BreadMageProyectileMovement>().targetPos = player.transform.position+new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0);
                        }
                    }
                }
                if(Random.Range(0f,1f)<=destroyProb)
                {
                    if(GetComponent<SpawnOnDestroy>()!=null)
                    {
                        GetComponent<SpawnOnDestroy>().spawnList = null;
                    }
                    Destroy(gameObject);
                }
            }

        }
    }
}
