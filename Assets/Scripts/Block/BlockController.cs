using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform altpoint;
    Animator anim;

    Vector3 hareketYonu = Vector3.up;
    Vector3 orjinalPos;
    Vector3 animPos;

    public LayerMask playerLayer;

    bool isAnimStart;
    bool hareketDevam = true;

    //blocktan coin cikmasi
    public GameObject coinPrefab;
    Vector3 coinPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        orjinalPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;

        //block carpttiktan sonra blok ustunde 1flik posda coin ciksin
        coinPos = transform.position;
        coinPos.y += 1f;
    }

    private void Update()
    {
        CarpistiMi();
        AnimControl();
    }

    void CarpistiMi()
    {
        if (hareketDevam)
        {
            //sadece player gectiginde calis, 1f lik isin
            RaycastHit2D hit = Physics2D.Raycast(altpoint.position, Vector2.down, .1f, playerLayer);

            //ray gormek icin
                //Debug.DrawRay(altpoint.position, Vector2.down, Color.green);

            //carpisma gerceklesti ve carpan player ise
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                anim.Play("mat");
                isAnimStart = true;
                hareketDevam = false;

                Instantiate(coinPrefab, coinPos, Quaternion.identity);
            }
        }
    }


    void AnimControl()
    {
        if (isAnimStart)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);

            //carpilan block tekrar asagi yonde hareket etsin
            if (transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down;
            }
            else if (transform.position.y <= orjinalPos.y)
            {
                isAnimStart = false; 
            }
        }
        
    }
}
