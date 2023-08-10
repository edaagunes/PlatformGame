using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Transform[] positions;

    public float birdSpeed;
    public float birdStayTime;
    float birdStayCounter;
    int whichPos;

    Animator anim;

    Vector2 birdDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        foreach (Transform pos in positions)
        {
            //kusun pozisyonlarini dizi disina at,kusun farkli poslarda gezmesini saglamak icin
            pos.parent = null;
        }
    }

    private void Start()
    {
        whichPos = 0;

        //ilk basladigi pos a gitsin
        transform.position = positions[whichPos].position;
    }

    private void Update()
    {
        if (birdStayCounter > 0)
        {
            birdStayCounter -= Time.deltaTime;
            anim.SetBool("isFly", false);
        }
        else
        {
            //kusun ucarken yonunu degistirme
            //iki nokta arasindaki vector
            birdDirection = new Vector2(positions[whichPos].position.x-transform.position.x, positions[whichPos].position.y - transform.position.y);

            //iki nokta arasindaki aci
            //arctan ile vector arasindaki aci --> radyaný dereceye cevir
            float angle=Mathf.Atan2(birdDirection.y,birdDirection.x)*Mathf.Rad2Deg;

            
            if (transform.position.x > positions[whichPos].position.x)
            {
                //geri donuslerde kusun y degerini tersine cevirip ters gorunmesini engelledi
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one; //Vector3(1,1,1)
            }

            //tek bu satir old. kus geri donuste ters donuyor z=170 deg oluyor
            transform.rotation = Quaternion.Euler(0, 0,angle);


            //kusu bir noktadan diger noktaya hareket ettirme
            transform.position = Vector3.MoveTowards(transform.position,
                positions[whichPos].position, birdSpeed * Time.deltaTime);

            anim.SetBool("isFly", true);

            //kus ulasmaya calistigi noktaya 0.1f lik uzakliktayken 
            if (Vector3.Distance(transform.position, positions[whichPos].position) < 0.1f)
            {
                //kus beklesin
                birdStayCounter = birdStayTime;
                ChangePos();
            }

        }
    }

    void ChangePos()
    {
        //sonraki pos dan devam edecek
        whichPos++;
        //diziyi astiysa pos basa donsun
        if (whichPos >= positions.Length)
        {
            whichPos = 0;
        }
    }
}
