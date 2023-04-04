﻿using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Dagger : WeaponDamage
    {
        [Tooltip("Multiplier that should be applied to damage when player is in stealth mode.")] 
        public float stealthDamageMultiplier;
        protected override void OnTriggerEnter(Collider other)
        {
            foreach (var tag in opponentTags)
            {
                if (other.gameObject.CompareTag(tag))
                {
                    if (other.GetComponentInParent<IHealthManager>() is IHealthManager opponent)
                    {
                        var player = FindObjectOfType<PlayerManager>();
                        if (player.InStealth)
                        {
                            DealStealthDamage(opponent);
                        }
                        else
                        {
                            DealDamage(opponent);
                        }
                    }
                }
            }
        }

        private void DealStealthDamage(IHealthManager opponent)
        {
            float damage = Random.Range(damageMin, damageMax) * stealthDamageMultiplier;
            opponent.DamageHealth(damage);
        }
    }
}
