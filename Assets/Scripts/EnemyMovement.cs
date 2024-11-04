using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;  // Vitesse de déplacement
    private string movementPattern = "default";  
    private float angle = 0f;  

    void Update()
    {
        switch (movementPattern)
        {
            case "spiral":
                // Mouvement en spirale descendante
                angle += speed * Time.deltaTime;  // Ajuster la vitesse pour ralentir le mouvement
                float x = Mathf.Cos(angle) * 0.1f; // Déplacement horizontal
                float y = Mathf.Sin(angle) * 0.1f - speed * Time.deltaTime; // Déplacement vertical
                transform.Translate(new Vector3(x, y, 0));
                break;

            case "cross":
                // Mouvement en croix (diagonale descendante rapide)
                transform.Translate(new Vector3(-0.5f, -1, 0) * speed * Time.deltaTime);
                break;

            case "horizontal":
                // Mouvement en ligne droite vers le bas
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;

            default:
                // Mouvement par défaut
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;
        }

        // Détruire l'ennemi s'il sort de l'écran (en dessous)
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    public void SetMovementPattern(string pattern)
    {
        movementPattern = pattern;
    }
}
