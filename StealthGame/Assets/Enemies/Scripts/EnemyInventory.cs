using Assets.Scripts.Enums;
using UnityEngine;

public class EnemyInventory : WeaponsInventory
{
    public GameObject sheathedSwordModel;
    public EnemyAnimatorManager enemyAnimatorManager;

    protected override void Awake()
    {
        base.Awake();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        
    }

    protected override void Start()
    {
        enemyAnimatorManager.SetBool("IsUnarmed", true);
        weaponSlotManager.LoadWeaponOnSlot(leftUnarmedItem, WeaponHand.Left);
        weaponSlotManager.LoadWeaponOnSlot(rightUnarmedItem, WeaponHand.Right);
    }

    public override void Disarm()
    {
        base.Disarm();
        sheathedSwordModel.SetActive(true);
        enemyAnimatorManager.PlayTargetAnimation("SheathSword", false);
    }

    public override void TakeOutWeapon()
    {
        sheathedSwordModel.SetActive(false);
        enemyAnimatorManager.PlayTargetAnimation("WithdrawSword", false);
        base.TakeOutWeapon();
    }


}
