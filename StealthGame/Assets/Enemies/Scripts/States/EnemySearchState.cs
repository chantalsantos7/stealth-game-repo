using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchState : EnemyState
{
    NavMeshAgent agent;
    SuspicionSystem suspicionSystem;
    DetectionSystem detectionSystem;
    //WeaponsInventory weaponsInventory;

    float suspicionThreshold;

    float patrolThreshold;
    float chaseThreshold;
    //if the enemy heard something, they should start navigating towards the position of where they heard something
    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        suspicionSystem = context.suspicionSystem;
        detectionSystem = context.detectionSystem;

        chaseThreshold = context.chaseSuspicionThreshold;
        patrolThreshold = context.patrolSuspicionThreshold;
        context.enemyInventory.TakeOutWeapon();
        //context.
        Debug.Log("Entered Search state");
    }
    
    public override void UpdateState(EnemyStateManager context)
    {
       //lastKnownPosition is only set if they heard something
        
        agent.SetDestination(detectionSystem.lastKnownPosition);

        //if they heard something

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

       

        //if they cannot currently see or hear player and it has been some time, go back to patrolling
       /* if (!detectionSystem.canSeePlayer && !detectionSystem.heardSomething && suspicionSystem.suspicionMeter < patrolThreshold)
        {
            context.SwitchState(context.patrolState);
        }*/
        //if can see player and suspicion above 

        //exit the search state when the suspicion is 0
        
    }

    public override void ExitState(EnemyStateManager context)
    {
        //suspicionSystem.suspicionMeter = 0;
        //only disarm if next state is patrol or idle
        
        Debug.Log("Exiting Search State");
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
