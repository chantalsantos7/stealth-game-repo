using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchState : EnemyState
{
    NavMeshAgent agent;
    SuspicionSystem suspicionSystem;
    DetectionSystem detectionSystem;
    
    float patrolThreshold;
    float chaseThreshold;
    
    public override void EnterState(EnemyStateManager context)
    {
        GameManager.Instance.EnemyIsSuspicious = true; //will activate suspicion indicator
        agent = context.agent;
        suspicionSystem = context.suspicionSystem;
        detectionSystem = context.detectionSystem;

        chaseThreshold = context.chaseSuspicionThreshold;
        patrolThreshold = context.patrolSuspicionThreshold;
        context.audioManager.PlayAudio("Suspicion");
        context.enemyInventory.TakeOutWeapon();
    }
    
    public override void UpdateState(EnemyStateManager context)
    {   
        agent.SetDestination(detectionSystem.lastKnownPosition);

        if (detectionSystem.canSeePlayer && suspicionSystem.suspicionMeter > chaseThreshold)
        {
            context.SwitchState(context.chaseState); 
        }

        if (suspicionSystem.suspicionMeter < patrolThreshold && !detectionSystem.canSeePlayer && !detectionSystem.heardSomething)
        {
            context.SwitchState(context.patrolState);
        }

        if (detectionSystem.canSeePlayer && detectionSystem.inAttackRange)
        {
            context.SwitchState(context.attackState);
        }
    }

    public override void ExitState(EnemyStateManager context)
    {
        GameManager.Instance.EnemyIsSuspicious = false;
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
