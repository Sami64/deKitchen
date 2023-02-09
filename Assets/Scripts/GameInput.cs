using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();

        return input.normalized;
    }
}
