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

    private Vector3 target;
    
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

        target = detectionSystem.lastKnownPosition;
        agent.SetDestination(target);

    }
    
    public override void UpdateState(EnemyStateManager context)
    {
        
        //after it reaches the last known position, move around randomly until suspicion meter is down
        Vector3 distanceToWalkPoint = agent.transform.position - target;

        //walk to a position
        if (distanceToWalkPoint.magnitude < 1f)
        {

        }

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

    private void GenerateWalkpoints()
    {

    }

    public override void ExitState(EnemyStateManager context)
    {
        GameManager.Instance.EnemyIsSuspicious = false;
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
