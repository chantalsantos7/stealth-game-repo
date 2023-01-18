using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerManager playerManager;
    private float health;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        health = playerManager.maxHealth;
    }

    private void DamageHealth(float amount)
    {
        health -= amount;
    }

    private void Die()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //check tag if other object is a weapon, then get their min/max damage, randomise how much damage the entity takes
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //get their weaponsDamage component
            if (collision.gameObject.TryGetComponent<WeaponsDamage>(out var weapon))
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
