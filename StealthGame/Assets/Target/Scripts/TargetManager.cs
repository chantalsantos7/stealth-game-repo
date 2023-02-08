using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManager : MonoBehaviour, IHealthManager
{
    public TargetStateManager stateManager;
    public float maxHealth;
    [SerializeField] protected float health;

    private void Awake()
    {
        stateManager = GetComponent<TargetStateManager>();
    }

    private void Start()
    {
        SetRigidbodyState(false);
        SetColliderState(false);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void DamageHealth(float amount)
    {
        health -= amount;
    }

    public void Die()
    {
        health = 0;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        SetRigidbodyState(true);
        SetColliderState(true);
        stateManager.SwitchState(stateManager.deathState);
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

    public void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !state;
            //rb.useGravity = false;
        }
    }

    // Start is called before the first frame update
    
}
