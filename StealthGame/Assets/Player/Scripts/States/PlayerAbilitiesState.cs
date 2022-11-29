
using UnityEngine;

public abstract class PlayerAbilitiesState
{
    public PlayerAbilitiesStateManager context { get; set;  }

    public abstract void EnterState(PlayerAbilitiesStateManager stateManager);

    public abstract void ExitState();

    public abstract void UpdateState();

    public abstract void OnCollisionEnter(Collision collision);
}
