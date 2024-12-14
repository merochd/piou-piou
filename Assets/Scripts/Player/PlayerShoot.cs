//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject basicBulletPrefab;
    [SerializeField] public float basicCooldown;
    [SerializeField] private GameObject piecingBulletPrefab;
    [SerializeField] public float piercingCooldown;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] public float shieldCooldown;
    public float timerShoot;
    public float abilityTimer;
    public bool canShoot = true;
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
            if (abilityTimer > 0)
            {
                abilityTimer -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.Space)) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ShootPiercing();
                return;
            }
            Shoot();// basic shoot
        }
        if (Input.GetKey(KeyCode.B))
        {
            Shield();
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {
            Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            timerShoot = basicCooldown;
        }
    }
    private void Shield()
    {
        if (abilityTimer <= 0)
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            abilityTimer = shieldCooldown;
        }
    }
    private void ShootPiercing()
    {
        if (canShoot)
        {
            Instantiate(piecingBulletPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            timerShoot = piercingCooldown;
        }
    }
}

//faire le cd par type de balle