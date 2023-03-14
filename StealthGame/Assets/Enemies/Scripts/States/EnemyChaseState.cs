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
        player = GameManager.Instance.player.transform;
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {

    }

    public override void UpdateState(EnemyStateManager context)
    {
        agent.speed = 4f;
        agent.SetDestination(player.position);
        
        if (context.detectionSystem.inAttackRange)
        {
            context.SwitchState(context.attackState);
        }
    }

    public override void ExitState(EnemyStateManager context)
    {
        agent.speed = 1.75f;
    }
}
