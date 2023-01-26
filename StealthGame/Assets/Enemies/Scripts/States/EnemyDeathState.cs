using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public override void EnterState(EnemyStateManager context)
    {
        Debug.Log("Entered death state");
    }

    public override void UpdateState(EnemyStateManager context)
    {
       
    }

    public override void ExitState(EnemyStateManager context)
    {
        
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
