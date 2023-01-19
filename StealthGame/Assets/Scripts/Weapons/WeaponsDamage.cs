using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDamage : MonoBehaviour
{
    [Tooltip("Minimum amount of damage the weapon can deal.")] public float damageMin;
    [Tooltip("Maximum amount of damage the weapon can deal.")] public float damageMax;
    //private Collider weaponCollider;

    protected void Awake()
    {
        //weaponCollider = GetComponentInChildren<Collider>();
    }

    private void Start()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
    }

    /*protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
