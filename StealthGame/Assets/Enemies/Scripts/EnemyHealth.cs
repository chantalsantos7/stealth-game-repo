using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }


    private void DamageHealth(float amount)
    {
        health -= amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            if (other.gameObject.TryGetComponent<WeaponsDamage>(out var weapon))
            {
                if (weapon != null)
                {
                    float damage = Random.Range(weapon.damageMin, weapon.damageMax);
                    DamageHealth(damage);
                }
            }
        }
    }
}
