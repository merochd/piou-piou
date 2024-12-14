using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f; // Adjustable movement speed

    private Camera _mainCamera; // Reference to the main camera

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