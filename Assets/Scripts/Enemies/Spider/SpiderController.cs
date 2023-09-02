using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    [SerializeField]
    Transform[] positions;

    [SerializeField]
    Slider spiderSlider;

    public int maxHealth;
    int currentHealth;

    public float spiderSpeed;
    public float waitingTime;
    float waitingCounter;

    public float takipMesafesi=5f;

    Animator anim;

    int whichPos;

    Transform targetPlayer;

    BoxCollider2D spiderCollider;
    bool isCanAttack;

    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spiderCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        spiderSlider.maxValue = maxHealth;

        SliderUpdate();

        isCanAttack = true;
        targetPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in positions)
        {
            //referans noktalari oynatmamak icin nesnenin icinden cikarttik
            pos.parent = null;
        }
    }

    private void Update()
    {
        if (!isCanAttack)   
           return;
        

        if (waitingCounter>0)
        {
            //orumcek verilen noktada duruyor
            waitingCounter -= Time.deltaTime;
            anim.SetBool("isMove", false);
        }
        else
        {
            //player iki pos arasindayken
            if (targetPlayer.position.x > positions[0].position.x && targetPlayer.position.x < positions[1].position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, spiderSpeed * Time.deltaTime);
                anim.SetBool("isMove", true);

                //orumcek sprite yonunu degistirmek icin
                if (transform.position.x > targetPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < targetPlayer.position.x)
                {
                    transform.localScale = Vector3.one;
                }

            }
            else
            {
                anim.SetBool("isMove", true);

                //orumcek sprite yonunu degistirmek icin
                if (transform.position.x > positions[whichPos].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < positions[whichPos].position.x)
                {
                    transform.localScale = Vector3.one;
                }


                transform.position = Vector3.MoveTowards(transform.position, positions[whichPos].position, spiderSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, positions[whichPos].position) < 0.1f)
                {
                    waitingCounter = waitingTime;
                    ChangePosition();
                }
            }


            
        }
    }

    void ChangePosition()
    {
        whichPos++;

        if (whichPos >= positions.Length)
        {
            whichPos = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spiderCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && isCanAttack)
        {
            //surekli attack yapamamasý icin 
            isCanAttack = false;
            //orumcegin colliderý player maskine carptýgýnda geri tepki fonk ve can azaltma calýssýn
            anim.SetTrigger("isAttack");
            other.GetComponent<PlayerMovementController>().GeriTepki();
            other.GetComponent<PlayerHealthController>().HealthDecrease();

            StartCoroutine(AgainAttack());

        }
    }

    IEnumerator AgainAttack()
    {
        yield return new WaitForSeconds(1f);

        if (currentHealth>0)
        {
            isCanAttack = true;
        }
        
    }

    public IEnumerator GeriTepkiFnc()
    {
        isCanAttack = false;
        rb.velocity = Vector2.zero; //orumcek hasar aldiginda hareket edememesi icin

      //  yield return new WaitForSeconds(0.1f);

        currentHealth--;

        SliderUpdate();

        if (currentHealth<=0)
        {
            //orumcek oldu
            isCanAttack=false;
            currentHealth = 0;
            anim.SetTrigger("isDie");
            spiderCollider.enabled = false;
            spiderSlider.gameObject.SetActive(false);
            Destroy(gameObject,2f);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);
            }

            anim.SetBool("isMove", false);
            yield return new WaitForSeconds(0.25f);

            rb.velocity= Vector2.zero;
            isCanAttack=true;
        }
    }

    void SliderUpdate()
    {
        spiderSlider.value = currentHealth;
    }
}
