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
            Debug.Log("agent speed: " + agent.speed); 
            agent.speed = attackRunSpeed;
            agent.SetDestination(player.position);
        }
        
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBetweenAttacks && detectionSystem.inAttackRange)
        {
            Attack();
        }

        if (!detectionSystem.canSeePlayer && !detectionSystem.inAttackRange) //if player moves behind the enemy, then should the enemy still follow them?
        {
            context.SwitchState(context.suspiciousState); //only enter suspiciousState if player has been out of their range for a while
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
