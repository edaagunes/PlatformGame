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

    private void Awake()
    {
        Instance = this;
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
}
