using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerAnimatorManager playerAnimatorManager;
    public WeaponsInventory playerInventory;
    public PlayerLocomotion playerLocomotion;
    public bool isUnarmed = false;
    public bool allowedToAttack;
    [Tooltip("How long between allowed player attacks.")] public float attackCooldown;
    public float timeBetweenAttacks;

    private void Awake()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerInventory = GetComponent<WeaponsInventory>();
        playerLocomotion = GetComponent<PlayerLocomotion>();    
    }

    private void Start()
    {
        allowedToAttack = true;

    }
    private void Update()
    {
        timeBetweenAttacks += Time.deltaTime;
        if (timeBetweenAttacks > attackCooldown)
        {
            allowedToAttack = true;
        }
    }

    public void Attack()
    {
        //randomly select one of the available anims
        //select between the left and right weapons 'randomly'
        //no, to keep it consistent w/ stealth attack, which does need an indication of what weapon to pick
        WeaponItem weapon = SelectWeapon();
        int animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        playerAnimatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], false);
        timeBetweenAttacks = 0;
        allowedToAttack = false;
    }

    public void SneakAttack()
    {
        playerLocomotion.IsCrouched = false;
        Attack();
        Debug.Log("Sneak attack");
        Invoke("ResetCrouch", 1f);
       
    }

    private void ResetCrouch()
    {
        playerLocomotion.IsCrouched = true;
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
