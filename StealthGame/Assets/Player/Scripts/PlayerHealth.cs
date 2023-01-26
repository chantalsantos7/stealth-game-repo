using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthManager
{
    public float maxHealth;
    public float health;
    public Collider coll;

    private void Start()
    {
        health = maxHealth;
    }

    public void DamageHealth(float amount)
    {
        health -= amount;
    }

    public void Die()
    {
        health = 0;
    }
}
