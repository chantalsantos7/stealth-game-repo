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
        //throw new System.NotImplementedException();
        agent = context.agent;
        player = context.player;
        detectionSystem = context.detectionSystem;
        animatorManager = context.enemyAnimatorManager;
        inventory = context.enemyInventory;
        agent.speed = 0f;
        timeBetweenAttacks = context.timeBetweenAttacks;
        timeElapsed = 0;
        //context.inAttackState = true;

        if (context.enemyManager.isUnarmed)
        {
            context.enemyInventory.TakeOutWeapon();
        }
    }

    public override void UpdateState(EnemyStateManager context)
    {
        //follow player - or a position in front of the player
        //play an attack animation

        //if player moves out of attack range, move towards them (at walking speed)
        //Debug.Log(detectionSystem.inAttackRange);
        if (!detectionSystem.inAttackRange)
        {
            agent.speed = 2f;
            agent.SetDestination(player.position);
        }
        //play attack animation (call attack function)
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBetweenAttacks && detectionSystem.inAttackRange)
        {
            Attack();
        }

        //if enemy loses sight of the player, they should still try to find the player (probably through hearing)
        if (!detectionSystem.canSeePlayer && !detectionSystem.inAttackRange)
        {
            context.SwitchState(context.suspiciousState);
        }

        /*if (context.enemyManager.health < 0)
        {
            context.SwitchState(context.deathState);
        }*/
    }

    public override void ExitState(EnemyStateManager context)
    {
        //context.inAttackState = false;
        //throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        //throw new System.NotImplementedException();
    }  

    private void Attack()
    {
        weapon = inventory.weaponSlotManager.GetWeaponOnSlot(WeaponHand.Right);
        int animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        animatorManager.PlayTargetAnimation(weapon.attackAnimations[animIndex], false);
        timeElapsed = 0;
    }
}
