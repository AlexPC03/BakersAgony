using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public float holeForce;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyBasicLifeSystem>()!=null && collision.GetComponent<Rigidbody2D>() != null)
        {
            if(collision.GetComponent<BreadEnemy>()!=null)
            {
                collision.GetComponent<Rigidbody2D>().AddForce((new Vector3(transform.position.x, transform.position.y, collision.transform.position.z) - collision.transform.position).normalized*holeForce,ForceMode2D.Force);
            }
            else if(collision.GetComponent<FlyingEnemy>() == null)
            {
                collision.GetComponent<Rigidbody2D>().AddForce((new Vector3(transform.position.x, transform.position.y, collision.transform.position.z) - collision.transform.position).normalized * -holeForce/3, ForceMode2D.Force);
            }
            if(collision.GetComponent<zPosition>()!=null)
            {
                float a= collision.GetComponent<zPosition>().difference;
                if (collision.GetComponent<FlyingEnemy>() == null && collision.transform.position.x<=transform.position.x+0.2 && collision.transform.position.x >= transform.position.x - 0.2 && collision.transform.position.y+a <= transform.position.y + 0.2 && collision.transform.position.y+a >= transform.position.y - 0.2)
                {
                    collision.GetComponent<EnemyBasicLifeSystem>().cornWeight = 0;
                    collision.GetComponent<EnemyBasicLifeSystem>().vida = 0;
                    collision.SendMessage("TakeDamage", 99);
                }
            }
        }
        else if(collision.GetComponent<playerMovement>()==null && collision.GetComponent<Rigidbody2D>()!=null && collision.tag!="sword")
        {
            collision.GetComponent<Rigidbody2D>().AddForce((new Vector3(transform.position.x, transform.position.y, collision.transform.position.z) - collision.transform.position).normalized * holeForce, ForceMode2D.Force);
            if (collision.GetComponent<zPosition>() != null )
            {
                float a = collision.GetComponent<zPosition>().difference;
                if (collision.transform.position.x <= transform.position.x + 0.2 && collision.transform.position.x >= transform.position.x - 0.2 && collision.transform.position.y + a <= transform.position.y + 0.2 && collision.transform.position.y + a >= transform.position.y - 0.2)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
