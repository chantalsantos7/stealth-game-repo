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

    public override void EnterState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
        agent = context.agent;
        player = context.player;
        detectionSystem = context.detectionSystem;
        agent.speed = 0f;

        if (context.enemyManager.isUnarmed)
        {
            context.enemyInventory.TakeOutWeapon();
            
        }

        Debug.Log("Entered attack state");
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

        /*int animIndex;
        animIndex = (int)Random.Range(0f, weapon.attackAnimations.Count - 1);
        context.enemyAnimatorManager.PlayTargetAnimation()*/

        //if enemy loses sight of the player, they should still try to find the player (probably through hearing)
        if (!detectionSystem.canSeePlayer && !detectionSystem.inAttackRange)
        {
            context.SwitchState(context.suspiciousState);
        }
    }

    public override void ExitState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        //throw new System.NotImplementedException();
    }  
}
