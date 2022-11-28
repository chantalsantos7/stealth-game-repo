using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    public void Attack(WeaponItem weapon, bool isLeft)
    {
        //randomly select one of the available anims
        int weaponIndex;
        if (isLeft)
        {
            weaponIndex = (int)Random.Range(0f, weapon.leftAttackAnimations.Count);
            animatorManager.PlayTargetAnimation(weapon.leftAttackAnimations[weaponIndex], true);
        } else
        {
            weaponIndex = (int)Random.Range(0f, weapon.rightAttackAnimations.Count);
            animatorManager.PlayTargetAnimation(weapon.rightAttackAnimations[weaponIndex], true);
        }

    }

    public void StealthAttack(WeaponItem weapon)
    {
        //check if behind NPC - if not, do nothing

        if (weapon.isUnarmed)
        {
            //play non-lethal animation
        }
        //animatorManager.PlayTargetAnimation("StealthAttack", false);
    }
}
