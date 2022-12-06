using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void EnterState(EnemyStateManager context)
    {
        
    }

    public override void UpdateState(EnemyStateManager context)
    {
        context.SwitchState(context.patrolState);
        
    }

    public override void ExitState(EnemyStateManager context)
    {
        //called by SwitchState, so no need for checks on which state to transition to 
        
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
