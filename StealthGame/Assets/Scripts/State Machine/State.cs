using UnityEngine;

public abstract class State
{
    public abstract void EnterState(StateManager context);

    public abstract void UpdateState(StateManager context);
    public abstract void ExitState(StateManager context);

    public abstract void OnCollisionEnter(StateManager context, Collision collision);
}
