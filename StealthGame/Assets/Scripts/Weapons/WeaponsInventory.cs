using Assets.Scripts.Enums;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem leftUnarmedItem;
    public WeaponItem rightUnarmedItem;
    public AnimatorManager animatorManager;

    protected virtual void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    protected virtual void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, WeaponHand.Right);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, WeaponHand.Left);
    }

    public virtual void Disarm()
    {
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", true);
        weaponSlotManager.LoadWeaponOnSlot(leftUnarmedItem, WeaponHand.Left);
        weaponSlotManager.LoadWeaponOnSlot(rightUnarmedItem, WeaponHand.Right);
    }

    public virtual void TakeOutWeapon()
    { 
        weaponSlotManager.UnloadWeaponOnSlot();
        animatorManager.SetBool("IsUnarmed", false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, WeaponHand.Left);
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, WeaponHand.Right);
    }
}
