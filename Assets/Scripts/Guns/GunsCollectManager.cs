using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsCollectManager : MonoBehaviour
{
    [SerializeField] private bool isSword, isSpear, isBow; //toplanilan kilic mi mizrak mi
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (other != null && isSword)
            {
                other.GetComponent<PlayerMovementController>().TurnSwordPlayer();

                Destroy(gameObject);
            }
            
            if (other != null && isSpear)
            {
                other.GetComponent<PlayerMovementController>().TurnSpearPlayer();

                
            }
            
            if (other != null && isBow)
            {
                other.GetComponent<PlayerMovementController>().TurnBowPlayer();

                
            }
            
            Destroy(gameObject);
        }
    }
}
