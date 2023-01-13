using System.Collections;
using System.Collections.Generic;
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
        //base.Start();
        enemyAnimatorManager.SetBool("IsUnarmed", true);
        weaponSlotManager.LoadWeaponOnSlot(unarmedItem, false);
        weaponSlotManager.LoadWeaponOnSlot(unarmedItem, true);
    }

    public override void Disarm()
    {
        base.Disarm();
        sheathedSwordModel.SetActive(true);
        enemyAnimatorManager.PlayTargetAnimation("SheathSword", false);
    }

    public override void TakeOutWeapon()
    {
        base.TakeOutWeapon();
        
        sheathedSwordModel.SetActive(false);
        enemyAnimatorManager.PlayTargetAnimation("WithdrawSword", false);
    }


}
