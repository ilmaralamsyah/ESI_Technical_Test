using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float projectileLifeSpan;

    private float attackDamage; 
    private Vector3 direction;
    private float speed;

    private void Start()
    {
        Destroy(gameObject, projectileLifeSpan);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 targetPosition, float projectileSpeed)
    {
        this.speed = projectileSpeed;

        // Hitung arah menuju target dan normalisasikan
        direction = (targetPosition - transform.position).normalized;
    }

    public void SetAttackDamage(float attackDamage)
    {
        this.attackDamage = attackDamage;
    }
}
