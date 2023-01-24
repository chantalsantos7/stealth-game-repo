using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

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
                break;
            case WeaponHand.Right:
                rightHandSlot.LoadWeaponModel(weaponItem);
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
}
