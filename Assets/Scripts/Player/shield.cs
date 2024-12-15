using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Transform player;
    [SerializeField] private int HP = 3;
    [SerializeField] private float duration = 3;
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Trouver le joueur dans la scène
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("A player object with the tag 'Player' is not found in the scene!");
        }
        transform.position = player.position;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy Bullet"))
        {
            Destroy(other.gameObject);
            HP -= 1;
        }
    }
}
