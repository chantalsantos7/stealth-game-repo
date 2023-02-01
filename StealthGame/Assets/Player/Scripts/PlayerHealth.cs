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

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void DamageHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        health = 0;
        GameManager.Instance.HandlePlayerDeath();
        gameObject.SetActive(false);
    }
}
