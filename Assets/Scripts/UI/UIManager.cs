using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;
   [SerializeField]
   Slider playerSlider;

   [SerializeField]
   TMP_Text coinText;

   [SerializeField] private Transform butonlarPanel;
   
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TumButonlarinAlphasiniAzalt();
        butonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerMovementController.Instance.TurnNormalPlayer();
    }

    public void SliderUpdate(int currentValue, int maxValue)
    {
        playerSlider.maxValue = maxValue;
        playerSlider.value = currentValue;
    }

    public void CoinUpdate()
    {
        coinText.text = GameManager.Instance.collectedCoin.ToString();
    }

    void TumButonlarinAlphasiniAzalt()
    {
        foreach (Transform btn in butonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
        }
    }

    public void NormalButtonPressed()
    {
        TumButonlarinAlphasiniAzalt();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>()
            .alpha = 1f;
        
        PlayerMovementController.Instance.TurnNormalPlayer();
    } 
    public void KilicButtonPressed()
    {
        TumButonlarinAlphasiniAzalt();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>()
            .alpha = 1f;
        
        PlayerMovementController.Instance.TurnSwordPlayer();
    }
    
    public void OkButtonPressed()
    {
        TumButonlarinAlphasiniAzalt();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>()
            .alpha = 1f;
        
        PlayerMovementController.Instance.TurnBowPlayer();
    }
    
    public void MizrakButtonPressed()
    {
        TumButonlarinAlphasiniAzalt();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>()
            .alpha = 1f;
        
        PlayerMovementController.Instance.TurnSpearPlayer();
    }
    
    
    
}
