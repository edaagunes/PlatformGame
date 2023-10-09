using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    [SerializeField]
    GameObject shinnyEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("EnemyLayer")))
        {
            if (other.CompareTag("Spider"))
            {
                if (shinnyEffect)
                {
                    Instantiate(shinnyEffect, other.transform.position, Quaternion.identity);
                }
               
                //IEnumerator cagirimi
                StartCoroutine(other.GetComponent<SpiderController>().GeriTepkiFnc());
                
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("EnemyLayer")))
        {
            if (other.CompareTag("Bat"))
            {
                if (shinnyEffect)
                {
                    Instantiate(shinnyEffect, other.transform.position, Quaternion.identity);
                }
                other.GetComponent<BatController>().DecreaseHealth();

            }
        }
    }
}
