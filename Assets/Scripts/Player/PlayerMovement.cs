using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Adjustable movement speed

    private Camera _mainCamera; // Reference to the main camera
    private bool GodMode = false;

    private void Start()
    {
        // Get the main camera
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // Get input from Horizontal and Vertical axes (WASD or Arrow keys)
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        var direction = new Vector3(horizontal, vertical, 0f).normalized;

        // Move the player
        transform.position += direction * (speed * Time.deltaTime);

        // Clamp the player position to stay within the screen bounds
        ClampPositionToScreen();
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            GodMode = !GodMode;
            Debug.Log("GodMode value: " + GodMode + "\\n Amusez-vous !");
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet avec lequel l'ennemi entre en collision est un projectile du joueur
        if (other.CompareTag("enemy") | other.CompareTag("Ennemy Bullet"))
        {
            Debug.Log("Touché !!!");
            if (!GodMode)
            {
                Destroy(gameObject);  // Détruit le player
                Debug.Log("vous etes mort !");
                for (float i = 0f; i < 15f;)
                {
                    i += Time.deltaTime;
                }
                SceneManager.LoadScene("scene mehdi");
                
            }
            else
            {
                Debug.Log("''GodMode'' est activé, vous ne mourrez pas !");
            }
        }
    }

    private void ClampPositionToScreen()
    {
        // Get the screen bounds in world space
        Vector3 screenBoundsMin = _mainCamera.ScreenToWorldPoint(new Vector3(25f, 10f, _mainCamera.nearClipPlane));
        Vector3 screenBoundsMax = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - 25f, Screen.height - 25f, _mainCamera.nearClipPlane));

        // Clamp the player's position
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, screenBoundsMin.x, screenBoundsMax.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, screenBoundsMin.y, screenBoundsMax.y);

        // Apply the clamped position
        transform.position = clampedPosition;
    }
}