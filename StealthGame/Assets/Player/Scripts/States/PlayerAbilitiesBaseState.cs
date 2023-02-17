using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesBaseState : PlayerAbilitiesState
{
    float cooldown;

    //public override PlayerAbilitiesStateManager context { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        context.playerLocomotion.IsSprinting = false;
        context.teleportAllowed = false;
        context.distractionAllowed = false;
        GameManager.Instance.uiManager.SetTeleportIconTransparency(0);
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
            GameManager.Instance.uiManager.SetTeleportIconTransparency(49);
        }

        context.distractTimeElapsed += Time.deltaTime;

        if (context.distractTimeElapsed >= context.distractionCooldown)
        {
            context.distractionAllowed = true;
            context.distractTimeElapsed = 0;
        }

        /*if (stateManager.playerLocomotion.IsSprinting)
        {
            stateManager.SwitchState(stateManager.sprintingState);
        }*/
    }
}
