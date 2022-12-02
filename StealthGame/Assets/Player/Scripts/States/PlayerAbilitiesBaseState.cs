using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesBaseState : PlayerAbilitiesState
{
    float cooldown;

    //public override PlayerAbilitiesStateManager context { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        //context = stateManager;
        context.playerLocomotion.IsSprinting = false;
        context.teleportAllowed = false;
        //context.teleportParticles.Stop();
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        context.teleportTimeElapsed += Time.deltaTime;
        //Debug.Log(teleportTimeElapsed);
        if (context.teleportTimeElapsed >= context.teleportCooldown)
        {
            context.teleportAllowed = true;
            context.teleportTimeElapsed = 0;
        }

        /*if (stateManager.playerLocomotion.IsSprinting)
        {
            stateManager.SwitchState(stateManager.sprintingState);
        }*/
    }

    private void RechargeStamina(PlayerAbilitiesStateManager context, float amount)
    {
        context.player.AddStamina(amount);
    }

    private IEnumerator RechargeStamina(PlayerAbilitiesStateManager context)
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
