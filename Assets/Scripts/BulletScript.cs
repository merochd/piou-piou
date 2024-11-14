using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime, Space.Self);
    }
}