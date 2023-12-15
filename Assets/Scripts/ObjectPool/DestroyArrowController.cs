using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrowController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            other.gameObject.SetActive(false);
        }
    }
}