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
        Invoke(nameof(ResetCrouch), 1f);
    }

    private void ResetCrouch()
    {
        playerLocomotion.IsCrouched = true;
    }


    private WeaponItem SelectWeapon()
    {
        int randIndex = Random.Range(0, 2);
        return randIndex switch
        {
            0 => playerInventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Left),
            1 => playerInventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Right),
            _ => null,
        };
    }
}
