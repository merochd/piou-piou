using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f; // Adjustable movement speed

    private void Update()
    {
        // Get input from Horizontal and Vertical axes (WASD or Arrow keys)
        var horizontal = Input.GetAxisRaw("Horizontal"); // add clamp
        var vertical = Input.GetAxisRaw("Vertical"); // add clamp

        // Calculate movement direction
        var direction = new Vector3(horizontal, vertical, 0f).normalized;

        // Move the player
        transform.position += direction * (speed * Time.deltaTime);
    }
}