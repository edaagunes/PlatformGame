using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BreakableObjectsManager : MonoBehaviour
{
    [SerializeField] private bool isCraft, isKorkuluk;


    Animator anim;
    int kacinciVurus;

    [SerializeField] GameObject shinny;

    [SerializeField] GameObject coinPrefab;

    Vector2 patlamaMiktari = new Vector2(1, 4);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        kacinciVurus = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //player kilicla sandýgý 3.vurusunda kýrsýn
        //animasyon adi degil trigger adi!(shake)
        if (other.CompareTag("kilicVurusBox"))
        {
            if (isCraft)
            {
                if (kacinciVurus == 0)
                {
                    anim.SetTrigger("shake");
                    Instantiate(shinny, transform.position, transform.rotation);
                }
                else if (kacinciVurus == 1)
                {
                    anim.SetTrigger("shake");
                    Instantiate(shinny, transform.position, transform.rotation);
                }
                else
                {
                    //kirdiktan sonra tekrar coin cikmasini engellemek icin 
                    GetComponent<BoxCollider2D>().enabled = false;
                    anim.SetTrigger("break");

                    //sandik kirildiginda coin uret
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 randomVector = new Vector3(transform.position.x + (i - 1), transform.position.y,
                            transform.position.z);
                        GameObject coin = Instantiate(coinPrefab, randomVector, transform.rotation);

                        //coinlere hareket verme
                        //hareket vermek icin kinematic den dynamic e cektik
                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari *
                                                                    new Vector2(UnityEngine.Random.Range(1, 3),
                                                                        transform.localScale.y +
                                                                        UnityEngine.Random.Range(0, 3));
                    }
                }

                kacinciVurus++;
            }
            
            // Player korkuluðu parcaladiktan sonra effect ve coin ciksin

            
                if (isKorkuluk)
                {
                    if (kacinciVurus == 0)
                    {
                        
                        Instantiate(shinny, transform.position, transform.rotation);
                    }
                    else if (kacinciVurus == 1)
                    {
                       
                        Instantiate(shinny, transform.position, transform.rotation);
                    }
                    else
                    {
                        //kirdiktan sonra tekrar coin cikmasini engellemek icin 
                        GetComponent<BoxCollider2D>().enabled = false;
                        

                        //korkuluk kirildiginda coin uret
                        for (int i = 0; i < 3; i++)
                        {
                            Vector3 randomVector = new Vector3(transform.position.x + (i - 1), transform.position.y,
                                transform.position.z);
                            GameObject coin = Instantiate(coinPrefab, randomVector, transform.rotation);

                            //coinlere hareket verme
                            //hareket vermek icin kinematic den dynamic e cektik
                            coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                            coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari *
                                                                        new Vector2(UnityEngine.Random.Range(1, 3),
                                                                            transform.localScale.y +
                                                                            UnityEngine.Random.Range(0, 3));
                        }
                        
                        Destroy(gameObject);
                    }

                    kacinciVurus++;
                }
            }
        }
    }
