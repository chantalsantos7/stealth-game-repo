using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDamage : MonoBehaviour
{
    [Tooltip("Minimum amount of damage the weapon can deal.")] public float damageMin;
    [Tooltip("Maximum amount of damage the weapon can deal.")] public float damageMax;
    private Collider collider;

    protected void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    /*protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
