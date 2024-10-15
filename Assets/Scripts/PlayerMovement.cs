using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Vitesse de déplacement
    private Vector2 movement;  // Variable pour stocker le déplacement

    void Update()
    {
        // Récupérer les inputs du joueur
        movement.x = Input.GetAxis("Horizontal");  // Droite / Gauche
        movement.y = Input.GetAxis("Vertical");    // Haut / Bas
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement au Rigidbody
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

