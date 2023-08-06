using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public void HealthDecrease()
    {
        currentHealth--;

        if (currentHealth<0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
