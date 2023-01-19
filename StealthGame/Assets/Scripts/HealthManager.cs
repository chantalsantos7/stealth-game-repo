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
}
