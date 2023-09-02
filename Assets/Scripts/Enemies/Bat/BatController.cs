using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField]
    float followDistance = 6f;

    [SerializeField]
    float batSpeed;

    [SerializeField]
    Transform targetPlayer;

    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D batCollider;

    public float attackTime;
    float attackSayac;
    float distance;

    Vector2 hareketYonu;


    private void Awake()
    {
        targetPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (attackSayac<0)
        {
            //player ile bat arasýndaki mesafeyi olc
            distance = Vector2.Distance(transform.position,targetPlayer.position);
            
            if (distance<followDistance)
            {
                anim.SetTrigger("isFly");

                hareketYonu = targetPlayer.position - transform.position; 

                rb.velocity = hareketYonu * batSpeed;

            }

            //saldýrýya gectiginde yonu oyuncuya taraf olsun
            if (transform.position.x>targetPlayer.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }else if(transform.position.x < targetPlayer.position.x)
            {
                transform.localScale = Vector3.one;
            }

        }
        else
        {
            attackSayac -= Time.deltaTime;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
