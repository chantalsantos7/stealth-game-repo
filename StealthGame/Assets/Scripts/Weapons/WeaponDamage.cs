using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Minimum amount of damage the weapon can deal.")] public float damageMin;
    [Tooltip("Maximum amount of damage the weapon can deal.")] public float damageMax;
    [Tooltip("The tag of objects this weapon should interact with.")] public string[] opponentTags;

    Collider damageCollider;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other) {

        foreach (var tag in opponentTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                if (other.GetComponentInParent<IHealthManager>() is IHealthManager opponent)
                {
                    DealDamage(opponent);
                }
            }
        }
    }

    protected void DealDamage(IHealthManager opponent)
    {
        float damage = Random.Range(damageMin, damageMax);
        Debug.Log("Dealing non-stealth damage: " + damage);
        opponent.DamageHealth(damage);
    }

    public void EnableDamageCollider(bool value)
    {
        damageCollider.enabled = value;
    }


}
