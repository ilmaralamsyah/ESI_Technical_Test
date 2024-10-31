using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInputAction playerInputAction;

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Awake()
    {
        Instance = this;

        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
