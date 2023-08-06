using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance;

    Rigidbody2D rb;

    [SerializeField]
    Transform groundControlPoint;
    [SerializeField]
    Animator anim;
    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;
    [SerializeField]
    SpriteRenderer sr;

    public LayerMask groundMask;


    public float movementSpeed;
    public float jumpPower;

    bool isGround;
    bool isDoubleJump;
    bool isDirectionRight;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (geriTepkiSayaci <= 0)
        {
            Movement();
            Jump();
            ChangeDirection();
            
            sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,1f);
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;
            if (isDirectionRight)
            {
                rb.velocity = new Vector2(-geriTepkiGucu,rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }

        }




        anim.SetBool("isGround", isGround);
        anim.SetFloat("movementSpeed", Mathf.Abs(rb.velocity.x));
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * movementSpeed, rb.velocity.y); //xdeki hýzý,ydeki hýzý


    }

    //atese yaklastiginda yon degistir
    void ChangeDirection()
    {

        if (rb.velocity.x < 0)//Karakterin yonu degistirildi
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isDirectionRight = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);//Vector3.one
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
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        rb.velocity = new Vector2 (0, rb.velocity.y);
    }
}
