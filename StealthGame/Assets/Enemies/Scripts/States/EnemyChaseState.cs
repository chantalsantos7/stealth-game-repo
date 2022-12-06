using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public override void EnterState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager context)
    {
        Debug.Log("Chasing");
        //throw new System.NotImplementedException();
    }
}
