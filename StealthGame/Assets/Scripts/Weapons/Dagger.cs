using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Dagger : WeaponsDamage
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.TryGetComponent<HealthManager>(out var opponent))
                {
                    DealDamage(opponent);
                }
            }
        }
    }
}
