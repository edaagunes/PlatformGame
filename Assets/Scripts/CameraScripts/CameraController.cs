using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerMovementController player;
    [SerializeField]
    Collider2D boundsBox;

    float halfYukseklik, halfGenislik;

    Vector2 sonPos;
    [SerializeField]
    Transform backgrounds;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerMovementController>();
        
    }

    private void Start()
    {
        //kameranın +y yönündeki yüksekliğinde oyunu başlat(6 birim)
        halfYukseklik = Camera.main.orthographicSize;

        //kameranın +x yönündeki genişliği
        halfGenislik = halfYukseklik * Camera.main.aspect; //6*1.7

        //print(Camera.main.aspect);//aspect: yükseklikle eninin arasındaki oran
        //1920x1080 çözünürlükteyken 1920/1080=1.7  

        sonPos = transform.position;
    }

    private void Update()
    {
        if (player!=null)
        {
            //kameranın oyuncuyu takip etmesi
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),//kameranın kaymasını engelledik
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z);//z yönünde kameranın transformunu aldık,2d olduğundan ekranda görmek için

        }

        if (backgrounds != null)
        {
            BackgroundHareket();
        }
        
    }

    void BackgroundHareket()
    {
        Vector2 aradakiFark= new Vector2(transform.position.x-sonPos.x,transform.position.y-sonPos.y);
        backgrounds.position += new Vector3(aradakiFark.x, aradakiFark.y, 0f);//bir sonraki hareket arasındaki fark kadar kaydırmak için
        sonPos = transform.position;
    }
}
