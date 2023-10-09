using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField]
    float followDistance = 6f;

    [SerializeField]
    float batSpeed;

    [SerializeField]
    Transform targetPlayer;

    [SerializeField]
    GameObject potionPrefab;

    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D batCollider;

    public float attackTime;
    float attackSayac;
    float distance;

    Vector2 hareketYonu;
    public int maxHealth;
    public int currentHealth;


    private void Awake()
    {
        targetPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (attackSayac<0)
        {
            if (targetPlayer && currentHealth > 0 && !PlayerMovementController.Instance.isDie)
            {
                //player ile bat arasýndaki mesafeyi olc
                distance = Vector2.Distance(transform.position, targetPlayer.position);

                if (distance < followDistance)
                {
                    anim.SetTrigger("isFly");

                    hareketYonu = targetPlayer.position - transform.position;


                    //saldýrýya gectiginde yonu oyuncuya taraf olsun
                    if (transform.position.x > targetPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < targetPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    rb.velocity = hareketYonu * batSpeed;
                }

            }

        }
            else
            {
                attackSayac -= Time.deltaTime;
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                attackSayac = attackTime;
                anim.SetTrigger("isAttack");

                other.GetComponent<PlayerMovementController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().HealthDecrease();
            }
        }
    }

    public void DecreaseHealth()
    {
        currentHealth--;
        attackSayac = attackTime; //hemen atak yapamasýn

        rb.velocity = Vector2.zero;

        if (currentHealth <=0)
        {
            currentHealth = 0;
            batCollider.enabled = false; //animasyon devam etmesin diye

            Instantiate(potionPrefab, transform.position, Quaternion.identity); 

            anim.SetTrigger("isDie");
            Destroy(gameObject,3f);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
