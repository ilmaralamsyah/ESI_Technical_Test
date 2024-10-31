using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        health.onHit += Health_onHit;
    }

    private void Health_onHit(float currentHealth)
    {
        healthBar.fillAmount = currentHealth;
    }
}
