using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState
{
    bool alreadyAttacked;
    NavMeshAgent agent;
    Transform player;

    public override void EnterState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
        agent = context.agent;
        player = context.player;
        agent.speed = 0f;
    }

    public override void UpdateState(EnemyStateManager context)
    {
        //follow player - or a position in front of the player
        //play an attack animation
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
