using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDamage : MonoBehaviour
{
    [Tooltip("Minimum amount of damage the weapon can deal.")] public float damageMin;
    [Tooltip("Maximum amount of damage the weapon can deal.")] public float damageMax;
    
    

    protected virtual void OnTriggerEnter(Collider other) {}

    protected void DealDamage(IHealthManager opponent)
    {
        float damage = Random.Range(damageMin, damageMax);
        opponent.DamageHealth(damage);
    }
    /*protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
