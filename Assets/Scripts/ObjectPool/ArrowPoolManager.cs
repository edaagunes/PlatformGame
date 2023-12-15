using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowPoolManager : MonoBehaviour
{
    public static ArrowPoolManager instance;

    [SerializeField] GameObject arrowPrefab;

    private GameObject arrowObject;
    private List<GameObject> arrowPool = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        CreateArrow();
    }

    void CreateArrow()
    {
        for (int i = 0; i < 10; i++)
        {
            arrowObject = Instantiate(arrowPrefab);
            arrowObject.SetActive(false);
            arrowObject.transform.parent = transform;

            arrowPool.Add(arrowObject);
        }
    }

    public void ThrowArrow(Transform okCikisNoktasi, Transform parent)
    {
        for (int i = 0; i < arrowPool.Count; i++)
        {
            if (!arrowPool[i].gameObject.activeInHierarchy)
            {
                //ok yonu ayarlamasi
                arrowPool[i].transform.localScale = parent.localScale;
                arrowPool[i].gameObject.SetActive(true);
                arrowPool[i].gameObject.transform.position = okCikisNoktasi.position;

                //player in dondugu yonde firlatilsin
                if (parent.transform.localScale.x > 0)
                {
                    arrowPool[i].GetComponent<Rigidbody2D>().velocity =
                        okCikisNoktasi.right * transform.localScale.x * 15f;
                }

                else
                {
                    arrowPool[i].GetComponent<Rigidbody2D>().velocity =
                        -okCikisNoktasi.right * transform.localScale.x * 15f;
                }

                return;
            }
        }
    }
}