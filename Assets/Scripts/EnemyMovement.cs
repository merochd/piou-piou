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
        if (other.CompareTag("PlayerProjectile"))  // Assure-toi que les projectiles du joueur ont bien le tag "PlayerProjectile"
        {
            ScoreManager.AddScore(10);  // Augmente le score de 10 points (ou adapte la valeur comme tu le souhaites)
            Destroy(other.gameObject);  // Détruit le projectile
            Destroy(gameObject);  // Détruit l'ennemi
        }
    }
}