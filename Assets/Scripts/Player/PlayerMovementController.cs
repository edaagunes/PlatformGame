using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance;

    Rigidbody2D rb;
    [SerializeField] GameObject normalPlayer, swordPlayer, spearPlayer, bowPlayer;
    [SerializeField] GameObject kilicVurusBoxObje;

    [SerializeField] Transform groundControlPoint;

    [SerializeField]
    Animator
        playerAnim, swordAnim, spearAnim, bowAnim; //(tum adlandirmalari guncellemek icin cift tik->yeniden adlandir)

    [SerializeField] float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;
    [SerializeField] SpriteRenderer playerSprite, swordSprite, spearSprite, bowSprite;

    [SerializeField] private GameObject throwSpear;
    [SerializeField] private Transform mizrakCikisNoktasi;
    public LayerMask groundMask;

    [SerializeField] private GameObject throwArrow;
    [SerializeField] private Transform okCikisNoktasi;

    public float movementSpeed;
    public float jumpPower;

    bool isGround;
    bool isDoubleJump;
    bool isDirectionRight;
    public bool isDie;
    bool isAttack;
    private bool isThrowArrow; //ok atabilir mi

    [SerializeField] private float tirmanisHizi=3f;
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        isDie = false;
        isAttack = false;
        isThrowArrow = true;

        kilicVurusBoxObje.SetActive(false);
    }

    private void Update()
    {
        if (isDie)
        {
            //player oldu ise update fonksiyonundan cik
            return;
        }


        if (geriTepkiSayaci <= 0)
        {
            Movement();
            Jump();
            ChangeDirection();

            //idle ve kilic spritelarý geri tepkisi icin ayri ayri alfa ayari

            if (normalPlayer.activeSelf)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }

            if (swordPlayer.activeSelf)
            {
                swordSprite.color = new Color(swordSprite.color.r, swordSprite.color.g, swordSprite.color.b, 1f);
            }

            if (spearPlayer.activeSelf)
            {
                spearSprite.color = new Color(spearSprite.color.r, spearSprite.color.g, spearSprite.color.b, 1f);
            }

            if (bowPlayer.activeSelf)
            {
                bowSprite.color = new Color(bowSprite.color.r, bowSprite.color.g, bowSprite.color.b, 1f);
            }

            //kilic attack idle na gecsin
            if (Input.GetMouseButtonDown(0) && swordPlayer.activeSelf)
            {
                isAttack = true;
                kilicVurusBoxObje.SetActive(true);
            }
            else
            {
                isAttack = false;
            }

            //mizrak
            if (Input.GetKeyDown(KeyCode.E) && spearPlayer.activeSelf)
            {
                spearAnim.SetTrigger("mizrakAtti");
                Invoke("ThrowSpear", .5f);
            }

            //ok
            if (Input.GetKeyDown(KeyCode.E) && bowPlayer.activeSelf && isThrowArrow)
            {
                bowAnim.SetTrigger("okAtti");
                StartCoroutine(ThrowArrowLaterRoutine());
                //  Invoke("ThrowArrow",.7f);
            }

            if (bowPlayer.activeSelf)
            {
                if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("MerdivenLayer")))
                {
                    float h = Input.GetAxis("Vertical");

                    Vector2 tirmanisVector = new Vector2(rb.velocity.x, h * tirmanisHizi);
                    rb.velocity = tirmanisVector;
                    rb.gravityScale = 0f;
                    
                    
                    bowAnim.SetBool("isClimb",true);
                    bowAnim.SetFloat("upMoveSpeed",Mathf.Abs(rb.velocity.y));
                }
                else
                {
                    bowAnim.SetBool("isClimb",false);
                    rb.gravityScale = 1f;
                }
            }
            
            
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;
            if (isDirectionRight)
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
        }

        //player hangi sprite da anlamak icin
        if (normalPlayer.activeSelf)
        {
            //idle anim calissin
            playerAnim.SetBool("isGround", isGround);
            playerAnim.SetFloat("movementSpeed", Mathf.Abs(rb.velocity.x));
        }

        //player kilic da aktifse
        if (swordPlayer.activeSelf)
        {
            //kilic anim calissin
            swordAnim.SetBool("isGround", isGround);
            swordAnim.SetFloat("movementSpeed", Mathf.Abs(rb.velocity.x));
        }

        //player mizrak da aktifse
        if (spearPlayer.activeSelf)
        {
            //kilic anim calissin
            spearAnim.SetBool("isOnGround", isGround);
            spearAnim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        }

        //player ok da aktifse
        if (bowPlayer.activeSelf)
        {
            //ok anim calissin
            bowAnim.SetBool("isGround", isGround);
            bowAnim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        }

        if (isAttack && swordPlayer.activeSelf)
        {
            //attack animasyonunu tetikletme
            if (isAttack)
            {
                swordAnim.SetTrigger("isAttack");
            }
        }
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * movementSpeed, rb.velocity.y); //xdeki hýzý,ydeki hýzý
    }

    void ThrowSpear()
    {
        GameObject spear = Instantiate(throwSpear, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        spear.transform.localScale = transform.localScale;
        spear.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 7f;

        Invoke("TurnNormalPlayer", .1f);
    }

    // ok firlatma
    //void ThrowArrow()
    //{
    //    //Object Pooling kullanilarak
    //    ArrowPoolManager.instance.ThrowArrow(okCikisNoktasi,this.transform);
    //    
    //   // GameObject arrow = Instantiate(throwArrow,okCikisNoktasi.position,okCikisNoktasi.rotation);
    //   // arrow.transform.localScale = transform.localScale; //playerýn yonu nereye donerse ok da donsun
    //   // arrow.GetComponent<Rigidbody2D>().velocity = okCikisNoktasi.right * transform.localScale.x * 15f;
    //}

    // Ok atma sikligini azaltmak icin
    IEnumerator ThrowArrowLaterRoutine()
    {
        isThrowArrow = false;
        yield return new WaitForSeconds(.7f);
        ArrowPoolManager.instance.ThrowArrow(okCikisNoktasi, this.transform);

        //Object Pooling kullanilarak
        ArrowPoolManager.instance.ThrowArrow(okCikisNoktasi, this.transform);

        isThrowArrow = true;
    }


    //atese yaklastiginda yon degistir
    void ChangeDirection()
    {
        if (rb.velocity.x < 0) //Karakterin yonu degistirildi
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isDirectionRight = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); //Vector3.one
            isDirectionRight = true;
        }
    }

    void Jump()
    {
        isGround = Physics2D.OverlapCircle(groundControlPoint.position, .2f, groundMask);

        if (Input.GetButtonDown("Jump") && (isGround || isDoubleJump)) //space tusu
        {
            if (isGround)
            {
                isDoubleJump = true;
            }
            else
            {
                isDoubleJump = false;
            }

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;
        //hasar aldiktan sonra karakterin soluklasmasi

        if (normalPlayer.activeSelf)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f);
        }

        if (swordPlayer.activeSelf)
        {
            swordSprite.color = new Color(swordSprite.color.r, swordSprite.color.g, swordSprite.color.b, 0.5f);
        }

        if (spearPlayer.activeSelf)
        {
            spearSprite.color = new Color(spearSprite.color.r, spearSprite.color.g, spearSprite.color.b, 0.5f);
        }

        if (bowPlayer.activeSelf)
        {
            bowSprite.color = new Color(bowSprite.color.r, bowSprite.color.g, bowSprite.color.b, 0.5f);
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void PlayerDie()
    {
        //oldu ise hizi sifir olmali
        rb.velocity = Vector2.zero;
        isDie = true;


        //player hangi sprite da anlamak icin
        if (normalPlayer.activeSelf)
        {
            //idle die anim calissin
            playerAnim.SetTrigger("isDie");
        }

        //player kilic da aktifse
        if (swordPlayer.activeSelf)
        {
            //kilic die anim calissin
            swordAnim.SetTrigger("isDie");
        }

        if (spearPlayer.activeSelf)
        {
            //mizrak die anim calissin
            spearAnim.SetTrigger("canVerdi");
        }

        if (bowPlayer.activeSelf)
        {
            //mizrak die anim calissin
            bowAnim.SetTrigger("isDeath");
        }


        //IEnumatoru tetiklemek icin
        StartCoroutine(AgainLoadScene());
    }

    //player oldukten sonra tekrar sahneyi baslatsin
    IEnumerator AgainLoadScene()
    {
        yield return new WaitForSeconds(2f);

        //destroy edildiginde karakterin tum comp.gittigi icin alt satiri islemiyor,sahne yuklenmiyor
        //bunun yerine karakterin sprite rend. silinebilir
        //// Destroy(gameObject);

        //player sprite hiyerarside alt satirda oldugu icin inchildren ile erisebiliriz
        //sprite renderer kapatildiginda karakter ekrandan yok olacak ve sahne yeniden yuklenebilecek
        GetComponentInChildren<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //kilic topladiktan sonra kilicli player gecis
    public void TurnSwordPlayer()
    {
        normalPlayer.SetActive(false);
        swordPlayer.SetActive(true);
        spearPlayer.SetActive(false);
        bowPlayer.SetActive(false);
    }

    //her þeyi kapat mizrak ac
    public void TurnSpearPlayer()
    {
        normalPlayer.SetActive(false);
        swordPlayer.SetActive(false);
        spearPlayer.SetActive(true);
        bowPlayer.SetActive(false);
    }

    public void TurnNormalPlayer()
    {
        normalPlayer.SetActive(true);
        swordPlayer.SetActive(false);
        spearPlayer.SetActive(false);
        bowPlayer.SetActive(false);
    }

    public void TurnBowPlayer()
    {
        normalPlayer.SetActive(false);
        swordPlayer.SetActive(false);
        spearPlayer.SetActive(false);
        bowPlayer.SetActive(true);
    }

    //Player sahneler arasi geciste hareket etmesin
    public void PlayerStop()
    {
        if (normalPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            playerAnim.SetFloat("movementSpeed", 0f);
        }

        if (swordPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            swordAnim.SetFloat("movementSpeed", 0f);
        }

        if (spearPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            spearAnim.SetFloat("movementSpeed", 0f);
        }

        if (bowPlayer.activeSelf)
        {
            rb.velocity = Vector2.zero;
            bowAnim.SetFloat("moveSpeed", 0f);
        }
    }
}