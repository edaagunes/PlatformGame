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
        //kameranýn +y yönündeki yüksekliðinde oyunu baþlat(6 birim)
        halfYukseklik = Camera.main.orthographicSize;

        //kameranýn +x yönündeki geniþliði
        halfGenislik = halfYukseklik * Camera.main.aspect; //6*1.7

        //print(Camera.main.aspect);//aspect: yükseklikle eninin arasýndaki oran
        //1920x1080 çözünürlükteyken 1920/1080=1.7  

        sonPos = transform.position;
    }

    private void Update()
    {
        if (player!=null)
        {
            //kameranýn oyuncuyu takip etmesi
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),//kameranýn kaymasýný engelledik
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z);//z yönünde kameranýn transformunu aldýk,2d olduðundan ekranda görmek için

        }
        BackgroundHareket();
    }

    void BackgroundHareket()
    {
        Vector2 aradakiFark= new Vector2(transform.position.x-sonPos.x,transform.position.y-sonPos.y);
        backgrounds.position += new Vector3(aradakiFark.x, aradakiFark.y, 0f);//bir sonraki hareket arasýndaki fark kadar kaydýrmak için
        sonPos = transform.position;
    }
}
