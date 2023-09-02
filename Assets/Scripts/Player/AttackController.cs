using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("EnemyLayer")))
        {
            if (other.CompareTag("Spider"))
            {
                //IEnumerator cagirimi
                StartCoroutine(other.GetComponent<SpiderController>().GeriTepkiFnc());

            }
        }
    }
}
