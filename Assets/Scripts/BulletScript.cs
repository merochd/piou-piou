//using System.Collections;
//using System.Collections.Generic;
//using System;
//using Unity.VisualScripting;


using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] public int bulletType; // 0 = basic / 1 = piercing

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (bulletSpeed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("step 1");
        if (other.CompareTag("enemy"))
        {
            Debug.Log("step 2");
            if (bulletType == 0)
            {
                Debug.Log("bullet destroyed");
                Destroy(gameObject, 0);
            }
            else
            {
                Debug.Log("bullet pierced");
            }
        }
    }
}