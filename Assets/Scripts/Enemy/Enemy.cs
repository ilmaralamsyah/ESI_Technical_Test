using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Setting")]
    [SerializeField] private float enemySpeed = 100f;
    
    [SerializeField] private float enemyAttackDistance;
    [SerializeField] private float enemyAttackCooldown;
    [SerializeField] private float enemyChaseRange;

    private EnemyAnimation enemyAnimation;
    private EnemyAttack enemyAttack;
    private SpriteRenderer spriteRenderer;

    private bool isPlayerDead = false;
    private float cooldownCounter;

    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerHealth.Instance.onDeath += Instance_onDeath;
        cooldownCounter = enemyAttackCooldown;
    }

    private void Instance_onDeath()
    {
        isPlayerDead = true;
    }

    private void Update()
    {
        if (isPlayerDead) { return; }
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        Vector3 playerPosition = Player.Instance.transform.position;
        float playerDistance = Vector3.Distance(playerPosition, transform.position);

        FlipSprite(playerPosition);

        if (playerDistance <= enemyChaseRange)
        {
            if (playerDistance <= enemyAttackDistance)
            {
                enemyAnimation.PlayIdleAnimation();
                HandleAttack();
            }
            else
            {
                MoveTo(playerPosition);
                enemyAnimation.PlayChasingAnimation();
            }
        }
        else
        {
            enemyAnimation.PlayIdleAnimation();
        }
    }

    private void MoveTo(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * enemySpeed * Time.deltaTime;
    }

    private void HandleAttack()
    {
        cooldownCounter += Time.deltaTime;
        if(cooldownCounter >= enemyAttackCooldown)
        {
            cooldownCounter = 0;
            enemyAttack.AttackPlayer();
            enemyAnimation.PlayAttackAnimation();
        }
    }

    private void FlipSprite(Vector3 playerPosition)
    {
        float directionX = playerPosition.x - transform.position.x;

        if (directionX < 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else if (directionX > 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }
}
