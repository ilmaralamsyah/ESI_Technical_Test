using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private enum AttackType
    {
        CloseRangeAttack,
        LongRangeAttack,
    }

    [Header("General Attack Setting")]
    [SerializeField] private AttackType attackType;
    [SerializeField] private float attackDamage;

    [Header("Long Range Attack Setting")]
    [SerializeField] private EnemyProjectile projectilePrefab;
    [SerializeField] private float projectileSpeed; 
    [SerializeField] private Transform projectilePos;

    public void AttackPlayer()
    {
        switch (attackType)
        {
            case AttackType.CloseRangeAttack:
                CloseRangedAttack();
                break;

            case AttackType.LongRangeAttack:
                LongRangeAttack();
                break;
        }
    }

    private void CloseRangedAttack()
    {
        //handle in attack animation
    }

    private void LongRangeAttack()
    {
        Vector3 target = Player.Instance.transform.position;
        EnemyProjectile projectile = Instantiate(projectilePrefab, projectilePos.position, Quaternion.identity);
        projectile.SetAttackDamage(attackDamage);
        projectile.Initialize(target, projectileSpeed);
    }

    public void DamagingPlayer()
    {
        IDamageable damageable = PlayerHealth.Instance;
        damageable.TakeDamage(attackDamage);
    }
}
