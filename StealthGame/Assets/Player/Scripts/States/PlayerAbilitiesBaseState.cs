using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesBaseState : PlayerAbilitiesState
{
    float cooldown;

    //public override PlayerAbilitiesStateManager context { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void EnterState(PlayerAbilitiesStateManager stateManager)
    {
        context = stateManager;
        context.playerLocomotion.IsSprinting = false;
    }

    public override void ExitState()
    {
        
    }

    public override void OnCollisionEnter(Collision collision)
    {
        
    }

    public override void UpdateState()
    {
        /*cooldown += Time.deltaTime;
        if (stateManager.player.CurrentStamina < stateManager.player.maxStamina && cooldown >= stateManager.player.staminaRegenCooldown)
        {
            RechargeStamina(stateManager, 0.5f);
        }*/



        /*if (stateManager.playerLocomotion.IsSprinting)
        {
            stateManager.SwitchState(stateManager.sprintingState);
        }*/
    }

    private void RechargeStamina(float amount)
    {
        context.player.AddStamina(amount);
    }

    private IEnumerator RechargeStamina()
    {
        //invoke this per second when stamina is 0
        yield return new WaitForSeconds(3f);
        while (context.currentStamina < context.maxStamina)
        {
            context.currentStamina += context.maxStamina / 100;
            yield return context.regenTick;
        }
    }
}
