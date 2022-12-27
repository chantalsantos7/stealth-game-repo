using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    bool alreadyAttacked;

    public override void EnterState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
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
