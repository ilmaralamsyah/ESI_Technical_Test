using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileLifeSpan;



    private float attackDamage;
    private Vector3 direction;
    private float speed;
    private bool isPlayerDead = false;

    private void Start()
    {
        Destroy(gameObject, projectileLifeSpan);
    }

    private void Update()
    {
        if (isPlayerDead)
        {
            this.gameObject.SetActive(false);
            return;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }

    public void Initialize(Vector3 direction, float speed, float attackDamage)
    {
        this.direction = direction;
        this.speed = speed;
        this.attackDamage = attackDamage;
        SetRotation(direction);
    }

    private void SetRotation(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));
    }
}
