using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void EnterState(EnemyStateManager context)
    {
        if (!context.enemyManager.isUnarmed)
        {
            context.enemyInventory.Disarm();
        }
    }

    public override void UpdateState(EnemyStateManager context)
    {
        context.SwitchState(context.patrolState);
        
    }

    public override void ExitState(EnemyStateManager context)
    {
       
    }
}
