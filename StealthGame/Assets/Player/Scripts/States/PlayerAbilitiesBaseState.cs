using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesBaseState : PlayerAbilitiesState
{
    float cooldown;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        context.playerLocomotion.IsSprinting = false;
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        /*cooldown += Time.deltaTime;
        if (context.player.CurrentStamina < context.player.maxStamina && cooldown >= context.player.staminaRegenCooldown)
        {
            RechargeStamina(context, 0.5f);
        }*/



        /*if (context.playerLocomotion.IsSprinting)
        {
            context.SwitchState(context.sprintingState);
        }*/
    }

    private void RechargeStamina(PlayerAbilitiesStateManager context, float amount)
    {
        context.player.AddStamina(amount);
    }
}
