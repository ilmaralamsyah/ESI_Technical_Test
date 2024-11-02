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
    private CapsuleCollider2D capsuleCollider;



    private bool isPlayerDead = false;
    private float cooldownCounter;
    private bool isColliderEnabled;
    private float updateCounter;
    private float updateTimerMax = 2f; //agar fungsi update dipanggil setiap 2 detik jika enemy jauh dari player
    private bool isFarAway;

    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        PlayerHealth.Instance.onDeath += Instance_onDeath;
        cooldownCounter = enemyAttackCooldown;
        isColliderEnabled = capsuleCollider.enabled;
    }

    private void Instance_onDeath()
    {
        isPlayerDead = true;
    }

    private void Update()
    {
        if (isPlayerDead) { return; }
        if (isFarAway)
        {
            updateCounter += Time.deltaTime;
            if (updateCounter > updateTimerMax)
            {
                updateCounter = 0;
            }
            else { return; }
        }
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        Vector3 playerPosition = Player.Instance.transform.position;
        float playerDistance = Vector3.Distance(playerPosition, transform.position);

        FlipSprite(playerPosition);

        if (playerDistance <= enemyChaseRange)
        {
            isFarAway = false;
            PlayerInChaseRange(playerPosition, playerDistance);
        }
        else
        {
            isFarAway = true;
            if (isColliderEnabled)
            {
                capsuleCollider.enabled = false;
            }
            enemyAnimation.PlayIdleAnimation();
        }
    }

    private void PlayerInChaseRange(Vector3 playerPosition, float playerDistance)
    {
        if (!isColliderEnabled)
        {
            capsuleCollider.enabled = true;
        }
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

    private void MoveTo(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * enemySpeed * Time.deltaTime;
    }

    private void HandleAttack()
    {
        cooldownCounter += Time.deltaTime;
        if (cooldownCounter >= enemyAttackCooldown)
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
