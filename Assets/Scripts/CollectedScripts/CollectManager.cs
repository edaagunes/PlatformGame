using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField]
    bool isPotion;

    [SerializeField]
    bool isCoin;
    bool isCollected;
    [SerializeField]
    GameObject patlamaEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player") && !isCollected)
        {
            if (isCoin)
            {
                isCollected = true;

                //Toplanan coin sayisini arttirma
                GameManager.Instance.collectedCoin++;
                UIManager.Instance.CoinUpdate();

                Destroy(gameObject);
                Instantiate(patlamaEffect, transform.position, Quaternion.identity);
            }

            if (isPotion)
            {
                isCollected = true;

                PlayerHealthController.Instance.AddingHealth();

                Destroy(gameObject);
                Instantiate(patlamaEffect, transform.position, Quaternion.identity);
            }
           
        }
    }
}
