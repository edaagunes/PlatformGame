using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SalinanEngelController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 200f;
    private float zAngle;

    [SerializeField] private float minZAngle = -75f;
    
    [SerializeField] private float maxZAngle = 75f;

    // Engelin donme yonu rastgele olmasi icin(sagdan sola ya da soldan saga) 
    private void Start()
    {
        if (Random.Range(0,2) > 0)
        {
            turnSpeed *= -1;
        }
    }


    // Engelin z ekseninde belirlenen acilar kadar donmesi gerceklestirildi
    private void Update()
    {
        zAngle += Time.deltaTime * turnSpeed;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);

        if (zAngle<minZAngle)
        {
            turnSpeed = Mathf.Abs(turnSpeed);
        }

        if (zAngle>maxZAngle)
        {
            turnSpeed = -Mathf.Abs(turnSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player") && !other.GetComponent<PlayerMovementController>().isDie)
            {
                other.GetComponent<PlayerMovementController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().HealthDecrease();
            }
        }
    }
}
