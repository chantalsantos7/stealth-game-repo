using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState
{
    bool alreadyAttacked;
    DetectionSystem detectionSystem;
    NavMeshAgent agent;
    Transform player;
    EnemyAnimatorManager animatorManager;
    EnemyInventory inventory;
    WeaponItem weapon;
    float timeBetweenAttacks;
    float timeElapsed;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        player = GameManager.Instance.player.transform;
        detectionSystem = context.detectionSystem;
        animatorManager = context.enemyAnimatorManager;
        inventory = context.enemyInventory;
        agent.speed = 0f;
        timeBetweenAttacks = context.timeBetweenAttacks;
        timeElapsed = 0;

        if (context.enemyManager.isUnarmed)
        {
            context.enemyInventory.TakeOutWeapon();
        }
    }

    public override void UpdateState(EnemyStateManager context)
    {
        if (!detectionSystem.inAttackRange)
        {
            agent.speed = 2f;
            agent.SetDestination(player.position);
        }
        
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBetweenAttacks && detectionSystem.inAttackRange)
        {
            Attack();
        }

        if (!detectionSystem.canSeePlayer && !detectionSystem.inAttackRange)
        {
            context.SwitchState(context.suspiciousState);
        }
    }

    public override void ExitState(EnemyStateManager context)
    {
        
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }  

    private void Attack()
    {
        weapon = inventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Right);
        int animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        animatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], false);
        timeElapsed = 0;
    }
}
