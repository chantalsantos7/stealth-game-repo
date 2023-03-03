using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour, IHealthManager
{
    [Header("Object References")] 
    public EnemyStateManager stateManager;
    public EnemyAnimatorManager animatorManager;
    public WeaponsInventory enemyInventory;
    public DetectionSystem detectionSystem;
    public GameObject swordModel;

    public float maxHealth;
    public float health;

    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isUnarmed;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        detectionSystem = GetComponent<DetectionSystem>();
        animatorManager = GetComponent<EnemyAnimatorManager>();
        enemyInventory = GetComponent<WeaponsInventory>();
    }

    private void Start()
    {
        SetRigidbodyState(false);
        SetColliderState(false);
        health = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        isUnarmed = animatorManager.GetBool("IsUnarmed");
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
        GetComponentInChildren<Animator>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        SetRigidbodyState(true);
        SetColliderState(true);
        isDead = true;
        stateManager.SwitchState(stateManager.deathState);
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
