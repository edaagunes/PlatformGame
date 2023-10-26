using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : MonoBehaviour
{
    [SerializeField] private float boarWalkSpeed, boarRunSpeed;

    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float gorusMesafesi = 8f;

    [SerializeField] private BoxCollider2D boarCollider;

    public bool isBoarDie;

    public LayerMask playerLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isBoarDie = false;
    }

    private void Update()
    {
        if (isBoarDie)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left),
            gorusMesafesi, playerLayer);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.red);

        transform.localScale = new Vector3(-1, 1, 1);

        if (hit.collider && hit.collider.CompareTag("Player"))
        {
            rb.velocity = new Vector2(-boarRunSpeed, rb.velocity.y);
            anim.SetBool("isRun", true);
        }

        else
        {
            rb.velocity = new Vector2(-boarWalkSpeed, rb.velocity.y);
            anim.SetBool("isRun", false);
        }
    }
}