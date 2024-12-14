using UnityEngine;

public class HomingProjectileController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du projectile
    private Vector3 moveDirection; // Direction de déplacement

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Déplacer le projectile dans la direction spécifiée
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Détruire le projectile s'il sort de l'écran
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }

    // Méthode pour définir la direction du projectile
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}