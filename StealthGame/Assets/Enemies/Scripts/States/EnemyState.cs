using UnityEngine;

/* Base State for all current enemy behaviours. Basic state structure from iHeartGameDev: https://youtu.be/Vt8aZDPzRjI */
public abstract class EnemyState
{
    public abstract void EnterState(EnemyStateManager context);
    public abstract void ExitState(EnemyStateManager context);

    public abstract void UpdateState(EnemyStateManager context);
}
