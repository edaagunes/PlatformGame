using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    [SerializeField] private GameObject fadeImg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TurnWhite();
    }

    //Seffaf gorunumden mata gecis
    public void TurnBlack()
    {
        fadeImg.GetComponent<CanvasGroup>().alpha = 0f;
        fadeImg.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    //Mat gorunumden seffafa gecis
    public void TurnWhite()
    {
        fadeImg.GetComponent<CanvasGroup>().alpha = 1f;
        fadeImg.GetComponent<CanvasGroup>().DOFade(0f, 1f);
    }
}
