using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private float timerShoot = 0;
    private bool canShoot = true;
    [SerializeField] public float cooldown;
    private void Update()
    {
        if (canShoot == false)
        {
            if (timerShoot > 0)
            {
                timerShoot -= Time.deltaTime;
            }
            else
            {
                canShoot = true;
            }
        }
        if (Input.GetKey(KeyCode.Space)) //Down
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            timerShoot = cooldown;
        }
    }
}
