using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerAnimatorManager playerAnimatorManager;
    public WeaponsInventory playerInventory;
    public bool isUnarmed = false;
    public bool isAttacking;

    private void Awake()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerInventory = GetComponent<WeaponsInventory>();
    }

    public void Attack()
    {
        //randomly select one of the available anims
        //select between the left and right weapons 'randomly'
        //no, to keep it consistent w/ stealth attack, which does need an indication of what weapon to pick
        WeaponItem weapon = SelectWeapon();
        int animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        playerAnimatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], true);
    }

    private WeaponItem SelectWeapon()
    {
        //have array of weaponHolderSlot in slotManager
        int randIndex = Random.Range(0, 2);
        return randIndex switch
        {
            0 => playerInventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Left),
            1 => playerInventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Right),
            _ => null,
        };
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
