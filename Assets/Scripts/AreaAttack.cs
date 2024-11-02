using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private float areaAttackDamage;
    private float areaAttackSpeed;

    void Update()
    {
        pivot.transform.Rotate(Vector3.forward * areaAttackSpeed * Time.deltaTime * 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(areaAttackDamage);
        }
    }

    public void SetAreaAttackStats(float areaAttackDamage, float areaAttackSpeed)
    {
        this.areaAttackDamage = areaAttackDamage;
        this.areaAttackSpeed = areaAttackSpeed;
    }
}
