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
        //kameran�n +y y�n�ndeki y�ksekli�inde oyunu ba�lat(6 birim)
        halfYukseklik = Camera.main.orthographicSize;

        //kameran�n +x y�n�ndeki geni�li�i
        halfGenislik = halfYukseklik * Camera.main.aspect; //6*1.7

        //print(Camera.main.aspect);//aspect: y�kseklikle eninin aras�ndaki oran
        //1920x1080 ��z�n�rl�kteyken 1920/1080=1.7  

        sonPos = transform.position;
    }

    private void Update()
    {
        if (player!=null)
        {
            //kameran�n oyuncuyu takip etmesi
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),//kameran�n kaymas�n� engelledik
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z);//z y�n�nde kameran�n transformunu ald�k,2d oldu�undan ekranda g�rmek i�in

        }
        BackgroundHareket();
    }

    void BackgroundHareket()
    {
        Vector2 aradakiFark= new Vector2(transform.position.x-sonPos.x,transform.position.y-sonPos.y);
        backgrounds.position += new Vector3(aradakiFark.x, aradakiFark.y, 0f);//bir sonraki hareket aras�ndaki fark kadar kayd�rmak i�in
        sonPos = transform.position;
    }
}
