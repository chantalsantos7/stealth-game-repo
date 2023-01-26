using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDamage : MonoBehaviour
{
    [Tooltip("Minimum amount of damage the weapon can deal.")] public float damageMin;
    [Tooltip("Maximum amount of damage the weapon can deal.")] public float damageMax;
    [Tooltip("The layer this weapon should interact with.")] public LayerMask opponentLayer;
    [Tooltip("The tag of objects this weapon should interact with.")] public string tagName;
    
    protected void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(tagName))
        {
            //trying to use these logs cause errors, the layermask value seems to be changed? but can still get to the DealDamage code so unimportant
            /*Debug.Log("layer index is: " + opponentLayer.value);
            Debug.Log("Correctly colliding with " + LayerMask.LayerToName(opponentLayer));*/
            if (other.GetComponentInParent<IHealthManager>() is IHealthManager opponent)
            {
                DealDamage(opponent);
            }
        }
    }

    protected void DealDamage(IHealthManager opponent)
    {
        float damage = Random.Range(damageMin, damageMax);
        opponent.DamageHealth(damage);
    }

    /*protected bool CheckLayer(Collider other)
    {
        if (other.gameObject.layer == opponentLayer)
    }*/
    /*protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
