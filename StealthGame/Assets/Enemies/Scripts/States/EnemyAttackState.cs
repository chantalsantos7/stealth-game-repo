using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState
{
    DetectionSystem detectionSystem;
    NavMeshAgent agent;
    Transform player;
    EnemyAnimatorManager animatorManager;
    EnemyInventory inventory;
    WeaponItem weapon;
    float timeBetweenAttacks;
    float timeElapsed;
    float attackRunSpeed;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        player = GameManager.Instance.player.transform;
        detectionSystem = context.detectionSystem;
        animatorManager = context.enemyAnimatorManager;
        inventory = context.enemyInventory;
        agent.speed = 0f;
        attackRunSpeed = context.attackRunSpeed;
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
            agent.speed = attackRunSpeed;
            agent.SetDestination(player.position);
        }
        
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBetweenAttacks && detectionSystem.inAttackRange)
        {
            Attack();
        }
        
        /*If the player has been out of sight and attack range for at least a few seconds, the enemy will return to the searching state
         and search for the player, rather than directly attacking them */
        if (!detectionSystem.canSeePlayer && !detectionSystem.inAttackRange 
            && detectionSystem.timeSinceLastDetected >= context.ExitAttackStateInSeconds) 
        {
            context.SwitchState(context.searchState);
        }
    }

    public override void ExitState(EnemyStateManager context)
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
