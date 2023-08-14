/* Base State for handling the player's special powers: Distracting and Teleporting */
public abstract class PlayerAbilitiesState
{
    public abstract void EnterState(PlayerAbilitiesStateManager context);

    public abstract void ExitState(PlayerAbilitiesStateManager context);

    public abstract void UpdateState(PlayerAbilitiesStateManager context);
}
