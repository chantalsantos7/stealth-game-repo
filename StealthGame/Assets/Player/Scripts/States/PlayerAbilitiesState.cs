
using UnityEngine;

public abstract class PlayerAbilitiesState
{
    public abstract void EnterState(PlayerAbilitiesStateManager context);

    public abstract void ExitState(PlayerAbilitiesStateManager context);

    public abstract void UpdateState(PlayerAbilitiesStateManager context);

    public abstract void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision);
}
