using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    WeaponDamage leftHandWeapon;
    WeaponDamage rightHandWeapon;

    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.weaponHand == WeaponHand.Left)
            {
                leftHandSlot = weaponSlot;
            } 
            else if (weaponSlot.weaponHand == WeaponHand.Right)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, WeaponHand weaponHand)
    {
        switch (weaponHand)
        {
            case WeaponHand.Left:
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftHandDamageCollider();
                break;
            case WeaponHand.Right:
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightHandDamageCollider();
                break;
        }
    }

    public void UnloadWeaponOnSlot()
    {
        leftHandSlot.UnloadWeapon();
        rightHandSlot.UnloadWeapon();
    }

    public WeaponItem GetWeaponOnSlot(WeaponHand weaponHand)
    {
        return weaponHand switch
        {
            WeaponHand.Left => leftHandSlot.CurrentWeapon,
            WeaponHand.Right => rightHandSlot.CurrentWeapon,
            _ => null,
        };
    }

    #region Handle Weapon's Damage Collider

    private void LoadLeftHandDamageCollider()
    {
        leftHandWeapon = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponDamage>();
    }

    private void LoadRightHandDamageCollider()
    {
        rightHandWeapon = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponDamage>();
    }

    public void OpenLeftHandDamageCollider()
    {
        leftHandWeapon.EnableDamageCollider(true);
    }

    public void OpenRightDamageCollider()
    {
        rightHandWeapon.EnableDamageCollider(true);
    }

    public void CloseLeftHandDamageCollider()
    {
        leftHandWeapon.EnableDamageCollider(false);
    }

    public void CloseRightHandDamageCollider()
    {
        rightHandWeapon.EnableDamageCollider(false);
    }

    #endregion
}
