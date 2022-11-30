using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmedItem;
    public AnimatorManager animatorManager;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        animatorManager= GetComponent<AnimatorManager>();
    }

    void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }

    public void SwitchWeapon(WeaponItem weapon)
    {
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", true);
        weaponSlotManager.LoadWeaponOnSlot(weapon, true);
        weaponSlotManager.LoadWeaponOnSlot(weapon, false);
    }

    public void SwitchWeapon()
    {
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
    }
}
