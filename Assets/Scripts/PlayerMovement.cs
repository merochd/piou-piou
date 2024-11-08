using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f; // Adjustable movement speed

    private void Update()
    {
        // Get input from Horizontal and Vertical axes (WASD or Arrow keys)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

        // Move the player
        transform.position += direction * speed * Time.deltaTime;
    }
}