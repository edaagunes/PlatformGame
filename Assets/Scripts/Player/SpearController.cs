using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boar") && !other.GetComponent<BoarController>().isBoarDie)
        {
            Destroy(gameObject);
            other.GetComponent<BoarController>().BoarDie();
        }
    }
}
