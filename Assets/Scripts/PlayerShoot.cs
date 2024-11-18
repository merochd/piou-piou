//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject basicBulletPrefab;
    [SerializeField] public float basicCooldown;
    [SerializeField] private GameObject piecingBulletPrefab;
    [SerializeField] public float piercingCooldown;
    private float _timerShoot;
    private bool _canShoot = true;
    private void Update()
    {
        if (_canShoot == false)
        {
            if (_timerShoot > 0)
            {
                _timerShoot -= Time.deltaTime;
            }
            else
            {
                _canShoot = true;
            }
        }
        if (Input.GetKey(KeyCode.Space)) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ShootPiercing();
                return;
            } else if (Input.GetKey(KeyCode.LeftAlt))
            {
                return;
            }
            Shoot();// basic shoot
        }
    }

    private void Shoot()
    {
        if (_canShoot)
        {
            Instantiate(basicBulletPrefab, transform.position, Quaternion.identity);
            _canShoot = false;
            _timerShoot = basicCooldown;
        }
    }
    private void ShootPiercing()
    {
        if (_canShoot)
        {
            Instantiate(piecingBulletPrefab, transform.position, Quaternion.identity);
            _canShoot = false;
            _timerShoot = piercingCooldown;
        }
    }
}

//faire le cd par type de balle