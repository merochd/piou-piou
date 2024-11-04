using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement_manager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Start()
    {
        // Assigner le composant PlayerInput
        playerInput = GetComponent<PlayerInput>();

        // S'assurer que moveAction est configuré correctement
        moveAction = playerInput.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        // Exemple de récupération des données de mouvement
        Vector2 move = moveAction.ReadValue<Vector2>();
        Debug.Log("Déplacement : " + move);
    }
}