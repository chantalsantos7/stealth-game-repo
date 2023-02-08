using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Dagger : WeaponDamage
    {
        [Tooltip("Multiplier that should be applied to damage when player is in stealth mode.")] 
        public float stealthDamageMultiplier;
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(opponentTag))
            {
                //trying to use these logs cause errors, the layermask value seems to be changed? but can still get to the DealDamage code so unimportant
                /*Debug.Log("layer index is: " + opponentLayer.value);
                Debug.Log("Correctly colliding with " + LayerMask.LayerToName(opponentLayer));*/
                if (other.GetComponentInParent<IHealthManager>() is IHealthManager opponent)
                {
                    //condition to check whether it was a stealth attack
                    //get the player from a FindObjectsOfType call
                    var player = FindObjectOfType<PlayerLocomotion>();
                    if (player.IsCrouched)
                    {
                        DealStealthDamage(opponent);
                    } else 
                    {
                        DealDamage(opponent);
                    }
                }
            }
        }

        private void DealStealthDamage(IHealthManager opponent)
        {
            float damage = Random.Range(damageMin, damageMax) * stealthDamageMultiplier;
            Debug.Log("Damage dealt: " + damage);
            opponent.DamageHealth(damage);
        }
    }
}
