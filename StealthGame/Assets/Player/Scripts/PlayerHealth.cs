using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        GameManager.Instance.vignette.IncreaseVignetteIntensity(0.4f);
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        health = 0;
        GetComponentInChildren<Animator>().enabled = false;
        HandlePlayerDeath();
        gameObject.SetActive(false);
    }

    private void HandlePlayerDeath()
    {
        GameManager.Instance.HandlePlayerDeath();
    }

    public void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !state;
        }
    }

    public void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider coll in colliders)
        {
            coll.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
}
