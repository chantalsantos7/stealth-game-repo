using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerAnimatorManager playerAnimatorManager;
    public bool isUnarmed = false;

    private void Awake()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    public void Attack(WeaponItem weapon)
    {
        //randomly select one of the available anims
        int animIndex;
        animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        playerAnimatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], false);
    }

    public void StealthAttack(WeaponItem weapon)
    {
        //check if behind NPC - if not, do nothing

        if (weapon.isUnarmed)
        {
            //play non-lethal animation
        }
        playerAnimatorManager.PlayTargetAnimation("SneakAttack", true);
        //animatorManager.PlayTargetAnimation("StealthAttack", false);
    }
}
