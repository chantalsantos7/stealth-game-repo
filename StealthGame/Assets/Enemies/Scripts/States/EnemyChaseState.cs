using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    NavMeshAgent agent;
    Transform player;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        player = context.player;
        Debug.Log("Entered Chase state");
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager context)
    {
        //Debug.Log("Chasing");
        agent.speed = 3f;
        agent.SetDestination(player.position);
        //set velocity to 2 (or running speed) 


        //slow down velocity as enemy approaches player
        //switch to attacking state when in range of player
        if (context.playerInAttackRange)
        {
            context.SwitchState(context.attackState);
        }
        
        //throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager context)
    {
        agent.speed = 1.75f;
    }
}
