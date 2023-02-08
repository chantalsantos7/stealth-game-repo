using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : WeaponDamage
    {
        /*protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<IHealthManager>() is IHealthManager opponent)
                {
                    DealDamage(opponent);
                }
            }
        }*/
    }
}
