using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmedItem;
    public AnimatorManager animatorManager;

    protected virtual void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    protected virtual void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }

    public virtual void Disarm()
    {
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", true);
        weaponSlotManager.LoadWeaponOnSlot(unarmedItem, true);
        weaponSlotManager.LoadWeaponOnSlot(unarmedItem, false);
    }

    public virtual void TakeOutWeapon()
    { 
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
    }
}
