using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    //scripti statik yapmak
    public static PlayerHealthController Instance;

    public int maxHealth, currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        //UIManager nesnesi varsa gerceklestir ki giris sahnesinde olmadigi icin hata vermesin
        if (UIManager.Instance != null)
        {
            //Slider degerlerini saglik degerleri ile esledik
            UIManager.Instance.SliderUpdate(currentHealth, maxHealth);

        }
        

    }

    public void HealthDecrease()
    {
        currentHealth--;

        //Saglik azaldikca slider da yansýyacak
        UIManager.Instance.SliderUpdate(currentHealth, maxHealth);

        if (currentHealth<0)
        {
            currentHealth = 0;
            //  gameObject.SetActive(false);
            //cani bittiginde olsun
            PlayerMovementController.Instance.PlayerDie();

        }
    }

    public void AddingHealth()
    {
        currentHealth++;

        if (currentHealth  >= maxHealth)
            currentHealth = maxHealth;
        
        UIManager.Instance.SliderUpdate(currentHealth, maxHealth);
    }
}
