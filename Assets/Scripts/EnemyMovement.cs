using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;  // Vitesse de déplacement

    void Update()
    {
        // Déplacer l'ennemi vers le bas de l'écran
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Détruire l'ennemi s'il sort de l'écran (en dessous)
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}

