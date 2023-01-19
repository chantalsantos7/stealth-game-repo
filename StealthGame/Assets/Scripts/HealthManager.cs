using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void DamageHealth(float amount)
    {
        health -= amount;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("got hit");
            if (other.gameObject.TryGetComponent<WeaponsDamage>(out var weapon))
            {
                if (weapon != null)
                {
                    float damage = Random.Range(weapon.damageMin, weapon.damageMax);
                    DamageHealth(damage);
                }
            }
        }
    }*/
}
