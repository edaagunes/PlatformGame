using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField]
    bool isCoin;
    bool isCollected;
    [SerializeField]
    GameObject coinEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            Destroy(gameObject);
            Instantiate(coinEffect,transform.position,Quaternion.identity);
        }
    }
}
