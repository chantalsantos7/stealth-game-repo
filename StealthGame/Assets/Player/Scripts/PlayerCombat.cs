using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AnimatorManager animatorManager;
    public bool isUnarmed = false;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    public void Attack(WeaponItem weapon, bool isLeft)
    {
        //randomly select one of the available anims
        int animIndex;
        animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        animatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], false);
    }

    public void StealthAttack(WeaponItem weapon)
    {
        //check if behind NPC - if not, do nothing

        if (weapon.isUnarmed)
        {
            //play non-lethal animation
        }
        animatorManager.PlayTargetAnimation("SneakAttack", true);
        //animatorManager.PlayTargetAnimation("StealthAttack", false);
    }
}
