using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer lampRenderer;

    [SerializeField]
    Sprite lampOnSprite, lampOffSprite;

    private void Awake()
    {
        lampRenderer.sprite = lampOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lampRenderer.sprite = lampOnSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("CloseLamp", 0.5f);
        }
    }

    void CloseLamp()
    {
        lampRenderer.sprite = lampOffSprite;
    }
}
