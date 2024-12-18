using System.Collections;
using UnityEngine;
using System;
public class HomingEnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float screenTopFraction = 0.33f; // Le tiers supérieur de l'écran
    public float moveSpeed = 3f; // Vitesse du déplacement vers la position cible
    public float shootInterval = 1f; // Temps entre chaque tir (en secondes)
    [SerializeField] private int HP; 

    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Préfab de projectile

    private Camera mainCamera;
    private Vector3 targetPosition; // La position vers laquelle se déplacer
    private bool hasReachedTarget = false; // Vérifie si l'ennemi a atteint sa position cible
    private Transform player; // Référence au joueur

    public event Action<GameObject> OnEnemyDeath;

    void Start()
    {
        mainCamera = Camera.main;

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

        // Définir une position cible aléatoire dans le tiers supérieur de l'écran
        SetRandomTargetPosition();
    }
    
    public void Die()
    {
        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        // Se déplacer vers la position cible
        if (!hasReachedTarget)
        {
            MoveToTargetPosition();
        }
        else
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
            transform.rotation = targetRotation;
        }

        if (HP <= 0)
        {
            Die();
        }
    }

    void SetRandomTargetPosition()
    {
        // Obtenir les limites de l'écran en coordonnées monde
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0, 0));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(0.95f, 0.9f, 0));

        // Calculer les limites pour le tiers supérieur
        float minX = screenBottomLeft.x;
        float maxX = screenTopRight.x;
        float minY = screenTopRight.y - (screenTopRight.y - screenBottomLeft.y) * screenTopFraction;
        float maxY = screenTopRight.y;

        // Choisir une position aléatoire
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        // Définir la position cible
        targetPosition = new Vector3(randomX, randomY, 0);
    }

    void MoveToTargetPosition()
    {
        // Se déplacer vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Vérifier si l'ennemi a atteint sa position cible
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            hasReachedTarget = true;

            // Une fois la position atteinte, commence à tirer
            StartCoroutine(ShootRoutine());
        }
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || player == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned, or player is missing!");
            return;
        }
        // Instancier le projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calculer la direction vers le joueur
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Appliquer la direction au projectile
        HomingProjectileController projectileScript = projectile.GetComponent<HomingProjectileController>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(directionToPlayer);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("projectile"))
        {
            HP -= 1;
        }
    }
}
