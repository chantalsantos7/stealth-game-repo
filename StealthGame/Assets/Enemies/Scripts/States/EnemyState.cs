using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(EnemyStateManager context);
    public abstract void ExitState(EnemyStateManager context);

    public abstract void UpdateState(EnemyStateManager context);

    public abstract void OnCollisionEnter(EnemyStateManager context, Collision other);
}
