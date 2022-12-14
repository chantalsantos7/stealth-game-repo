using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintingState : PlayerAbilitiesState
{

    //get player locomotion from stateManager
    //PlayerAbilitiesStateManager stateManager;
    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        //throw new System.NotImplementedException();
        //this.context = context;
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        if (!context.playerLocomotion.IsSprinting)
        {
            context.SwitchState(context.baseState);
        }

        //UseStamina(context, 1f);

        if (context.player.CurrentStamina <= 0)
        {
            context.SwitchState(context.baseState);
        }

    }

/*    private void UseStamina(PlayerAbilitiesStateManager context, float amount)
    {
        context.player.UseStamina(amount);
    }*/

    private IEnumerator UseStamina(float amount)
    {
        //decrease the amount from current Stamina
        //currentStamina -= amount;
        yield return new WaitForSeconds(1f);
    }
}
