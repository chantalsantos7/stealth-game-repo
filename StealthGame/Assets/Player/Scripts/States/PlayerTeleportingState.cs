using Assets.Scripts.Enumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingState : PlayerAbilitiesState
{
    public float movementSpeed;
    public float rotationSpeed;
    public float gravity = -20f;
    float teleportTime;
    float timeCounter;
    CharacterController tpViewController;
    GameObject teleportView;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.AimTeleport;
        teleportView = context.teleportView;
        tpViewController = context.teleportViewController;
        teleportView.transform.position = context.transform.position + Vector3.right;

        context.playerLocomotion.canMove = false;
        movementSpeed = context.playerLocomotion.teleportMovementSpeed;
        rotationSpeed = context.playerLocomotion.teleportRotationSpeed;

        teleportView.SetActive(true);
        teleportTime = context.teleportTimeLimit;
        timeCounter = 0f;
        GameManager.Instance.uiManager.ToggleTeleportDeployText();
        context.audioSource.PlayOneShot(context.teleportModeSoundEffect);
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.Basic;
        context.teleportView.SetActive(false);
        GameManager.Instance.uiManager.ToggleTeleportDeployText();
        GameManager.Instance.uiManager.SetTeleportIconTransparency(0);
        context.playerLocomotion.canMove = true;
        context.teleportAllowed = false;
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= teleportTime)
        {
            Teleport(context);
        }

        Vector3 moveDirection = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        moveDirection += context.cameraManager.transform.right * context.inputManager.horizontalInput;
        moveDirection.Normalize();

        moveDirection += (Vector3.up * gravity);
        tpViewController.Move(movementSpeed * Time.deltaTime * moveDirection);
        
        if (context.inputManager.TeleportKeyPressed)
        {
            Teleport(context);
            context.inputManager.TeleportKeyPressed = false;
        }
    }

    
    private void Teleport(PlayerAbilitiesStateManager context)
    {
        context.playerLocomotion.playerRigidbody.position = teleportView.transform.position;
        GameManager.Instance.achievementTracker.TeleportUsed++;
        
        //Turn off the teleporting background sound, play the ability sound effect
        context.audioSource.Stop();
        context.audioSource.PlayOneShot(context.teleportSoundEffect);
        
        context.SwitchState(context.baseState);
    }
}
