using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static PlayerHealth Instance { get; private set; }

    public event Action <float> onHit;
    public event Action onDeath;
    
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        Instance = this;
        playerAnimation = GetComponent<PlayerAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        onHit?.Invoke(1f);//1f = 100% health bar
    }

    public void TakeDamage(float damageAmmount)
    {
        currentHealth -= damageAmmount;
        onHit?.Invoke(currentHealth/maxHealth);

        if (currentHealth <= 0)
        {
            InputManager.Instance.gameObject.SetActive(false);
            capsuleCollider.enabled = false;
            playerAnimation.TriggerDeathAnimation();
            onDeath?.Invoke();
        }
    }
}
