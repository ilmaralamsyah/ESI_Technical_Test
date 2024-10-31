using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{

    [Header("Sword Attack")]
    [SerializeField] private PlayerProjectile swordAttackProjectile;
    [SerializeField] private Transform projectilePos;
    [SerializeField] private float swordAttackCooldown;
    [SerializeField] private float swordAttackDamage;
    [SerializeField] private float swordAttackSpeed;

    [Header("Area Attack")]
    [SerializeField] private AreaAttack areaAttack;
    [SerializeField] private float areaAttackCooldown;
    [SerializeField] private float areaAttackUpTime;
    [SerializeField] private float areaAttackDamage;
    [SerializeField] private float areaAttackSpeed;

    private float swordAttackCounter;
    private float areaAttackCooldownCounter;
    private float areaAttackUpTimeCounter;
    private PlayerAnimation playerAnimation;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        areaAttackCooldownCounter = areaAttackCooldown;
        areaAttack.SetAreaAttackStats(areaAttackDamage, areaAttackSpeed);
    }

    private void Update()
    {
        HandleSwordAttack();
        HandleAreaAttack();
    }

    private void HandleSwordAttack()
    {
        swordAttackCounter += Time.deltaTime;
        if (swordAttackCounter >= swordAttackCooldown)
        {
            swordAttackCounter = 0;
            playerAnimation.TriggerSwordAttackAnimation();
        }
    }

    private void LaunchAttack()
    {
        Vector2 direction = player.GetMoveDirection();
        if(direction == Vector2.zero)
        {
            direction = Vector2.right;
        }
        PlayerProjectile projectile = Instantiate(swordAttackProjectile, projectilePos.position, Quaternion.identity);
        projectile.Initialize(direction, swordAttackSpeed, swordAttackDamage);
    }

    private void HandleAreaAttack()
    {
        areaAttackCooldownCounter += Time.deltaTime;
        if(areaAttackCooldownCounter >= areaAttackCooldown)
        {
            areaAttack.gameObject.SetActive(true);
            areaAttackUpTimeCounter += Time.deltaTime;
            if(areaAttackUpTimeCounter >= areaAttackUpTime)
            {
                areaAttack.gameObject.SetActive(false);
                areaAttackCooldownCounter = 0;
                areaAttackUpTimeCounter = 0;
            }
            
            
        }
    }
}
