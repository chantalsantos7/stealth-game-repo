using Assets.Scripts.Enumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingState : PlayerAbilitiesState
{
    public float movementSpeed = 3.5f;
    public float rotationSpeed = 5f;
    float teleportTime;
    float timeCounter;
    Rigidbody teleportRb;
    GameObject teleportView;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.AimTeleport;
        teleportView = context.teleportView;
        teleportView.transform.position = context.transform.position + Vector3.right;
        context.playerLocomotion.canMove = false;
        teleportView.SetActive(true);
        teleportTime = context.teleportTimeLimit;
        teleportRb = context.teleportRigidbody;
        timeCounter = 0f;
        GameManager.Instance.uiManager.ToggleTeleportDeploy();
        context.audioSource.PlayOneShot(context.teleportModeSoundEffect);
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.Basic;
        context.teleportView.SetActive(false);
        GameManager.Instance.uiManager.ToggleTeleportDeploy();
        GameManager.Instance.uiManager.SetTeleportIconTransparency(0);
        context.playerLocomotion.canMove = true;
        context.teleportAllowed = false;
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= teleportTime)
        {
            context.audioSource.Stop();
            Teleport(context);
            //context.inputManager.TeleportKeyPressed = false;
        }

        Vector3 moveDirection = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        moveDirection += context.cameraManager.transform.right * context.inputManager.horizontalInput;

        teleportRb.velocity = moveDirection * movementSpeed;

        Vector3 raycastOrigin = teleportRb.transform.position;
        
        if (!Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out RaycastHit hit, 
            context.playerLocomotion.maxDistance, context.playerLocomotion.groundLayers))
        {
            teleportRb.AddForce(context.playerLocomotion.fallingVelocity * Vector3.down);
            
        }
        
        if (context.inputManager.TeleportKeyPressed)
        {

            Debug.Log("Teleport Pressed");
            Teleport(context);
            context.inputManager.TeleportKeyPressed = false;
        }
    }

    private void Teleport(PlayerAbilitiesStateManager context)
    {
        Vector3 TeleportPos = teleportView.transform.position;
        context.playerLocomotion.playerRigidbody.position = TeleportPos;
        GameManager.Instance.achievementTracker.TeleportUsed++;
        context.audioSource.Stop();
        context.audioSource.PlayOneShot(context.teleportSoundEffect);
        context.SwitchState(context.baseState);
    }

    
}
