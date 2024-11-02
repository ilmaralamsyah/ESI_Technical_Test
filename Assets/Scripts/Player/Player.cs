using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private InputManager inputManager;

    [Header("Player Setting")]
    [SerializeField] private float moveSpeed = 5f;


    private Vector3 moveDir;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        Instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (moveDir.x < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void HandleInput()
    {
        Vector2 input = inputManager.GetMovementVectorNormalized();
        moveDir = new Vector3(input.x, input.y, 0f);
    }

    private void ProcessMovement()
    {
        rigidBody2D.velocity = new Vector2(moveDir.x, moveDir.y) * moveSpeed;
    }

    public Vector3 GetMoveDirection()
    {
        return moveDir;
    }
}
