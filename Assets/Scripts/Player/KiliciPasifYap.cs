using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiliciPasifYap : MonoBehaviour
{
    public GameObject kilicVurusBox;

    public void KiliciKapat()
    {
        kilicVurusBox.SetActive(false);
    }

}
