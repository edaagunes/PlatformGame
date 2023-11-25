using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public string otherScene;

    //Diger sahneye geciste player hareketsiz dursun
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovementController>().PlayerStop();
            other.GetComponent<PlayerMovementController>().enabled=false;
            
            FadeController.instance.TurnBlack();

            StartCoroutine(TurnOtherScene());
        }
    }

    IEnumerator TurnOtherScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(otherScene);
    }
}
