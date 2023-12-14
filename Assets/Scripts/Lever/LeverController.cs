using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeverController : MonoBehaviour
{
    [SerializeField] GameObject door;

    //Ok geldiginde kapi acilsin
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            GetComponent<Animator>().SetTrigger("openLever");
            GetComponent<BoxCollider2D>().enabled = false;
            door.transform.DOLocalMoveY(door.transform.localPosition.y + 0.4f,.5f);
        }
    }
}
