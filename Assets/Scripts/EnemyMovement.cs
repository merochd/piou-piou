using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;  // Vitesse de déplacement

    void Update()
    {
        // Déplacer l'ennemi vers le bas de l'écran
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Détruire l'ennemi s'il sort de l'écran (en dessous), sans ajouter de score
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet avec lequel l'ennemi entre en collision est un projectile du joueur
        if (other.CompareTag("projectile"))
        {
            Destroy(other.gameObject);  // Détruit le projectile
            Destroy(gameObject);  // Détruit l'ennemi
        }
    }
}