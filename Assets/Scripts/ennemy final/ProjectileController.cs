using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du projectile

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Déplacement vers le bas
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Détruire le projectile s'il est hors de l'écran
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.x < 0 || screenPosition.x > 1)
        {
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
   // {
        // Vérifie si l'objet avec lequel l'ennemi entre en collision est un projectile du joueur
        //if (other.CompareTag("Player"))
        //{
            //Destroy(other.gameObject);  // Détruit le player
            //Debug.Log("touché!!!");
        //}
    //}
}