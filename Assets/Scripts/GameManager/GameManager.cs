using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    public int collectedCoin;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        collectedCoin = 0;
    }
}
