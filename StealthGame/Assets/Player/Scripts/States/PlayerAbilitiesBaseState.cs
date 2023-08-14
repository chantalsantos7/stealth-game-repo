using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesBaseState : PlayerAbilitiesState
{
    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        context.playerLocomotion.IsSprinting = false;
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        context.teleportTimeElapsed += Time.deltaTime;
        if (context.teleportTimeElapsed >= context.teleportCooldown)
        {
            context.teleportAllowed = true;
            context.teleportTimeElapsed = 0;
            GameManager.Instance.uiManager.SetTeleportIconTransparency(0.15f);
        }

        context.distractTimeElapsed += Time.deltaTime;

        if (context.distractTimeElapsed >= context.distractionCooldown)
        {
            context.distractionAllowed = true;
            context.distractTimeElapsed = 0;
            GameManager.Instance.uiManager.SetDistractIconTransparency(0.15f);
        }
    }
}
