using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action<float> onHit;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private DropItem dropItem;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        onHit?.Invoke(1f);
    }

    public void TakeDamage(float damageAmmount)
    {
        currentHealth -= damageAmmount;
        onHit?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
