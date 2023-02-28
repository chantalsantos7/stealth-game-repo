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
        //SetRigidbodyState(false);
        //SetColliderState(false);
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
        GameManager.Instance.vignette.IncreaseVignetteIntensity(0.4f);
        /*if (health == maxHealth)
        {
            GameManager.Instance.vignette.IncreaseVignetteIntensity(0.4f);
        } else
        {
            GameManager.Instance.vignette.IncreaseVignetteIntensity(0.6f);
        }*/
        health -= amount;
        //
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        health = 0;
        GetComponentInChildren<Animator>().enabled = false;
       /* SetRigidbodyState(true);
        SetColliderState(true);*/
        HandlePlayerDeath();
        //Invoke("HandlePlayerDeath", 2f);
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
            //rb.useGravity = false;
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
