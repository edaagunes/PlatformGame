using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManagerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other != null)
            {
                other.GetComponent<PlayerMovementController>().TurnSwordPlayer();

                Destroy(gameObject);
            }
            
        }
    }
}
