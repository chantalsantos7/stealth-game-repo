using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingState : PlayerAbilitiesState
{
    float teleportRadiusLimit;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    //float teleportCooldown = 10f;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        Debug.Log("Teleport mode entered");
        context.cameraManager.cameraMode = CameraMode.AimTeleport;
        context.teleportView.transform.position = context.transform.position + Vector3.right;
        context.playerLocomotion.canMove = false;
        context.teleportView.SetActive(true);
        teleportRadiusLimit = context.player.teleportLimit;
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.Basic;
        context.teleportView.SetActive(false);
        context.teleportParticles.Stop();
        context.playerLocomotion.canMove = true;
        context.teleportAllowed = false;
        context.SwitchState(context.baseState);
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        if (Vector3.Distance(context.teleportRigidbody.position, context.playerLocomotion.playerRigidbody.position) > teleportRadiusLimit)
        {
            Debug.Log("Too far!"); //replace with a UI showing the distance you can travel
            context.teleportRigidbody.position -= new Vector3(0, 0, 0.2f); //move it back slightly, so it doesn't get stuck
            return;
        }
        //Debug.Log("Teleport state update being called");
        Vector3 movementVelocity = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        movementVelocity += context.cameraManager.transform.right * context.inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        context.teleportRigidbody.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = context.teleportRigidbody.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(context.teleportRigidbody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        context.teleportRigidbody.transform.rotation = playerRotation;

        if (context.inputManager.teleportKeyPressed)
        {
            Teleport(context);
            context.inputManager.teleportKeyPressed = false;
        }

    }

    private void Teleport(PlayerAbilitiesStateManager context)
    {
        context.teleportParticles.gameObject.SetActive(true);
        context.teleportParticles.Emit(1);

        Vector3 TeleportPos = context.teleportView.transform.position;
        context.playerLocomotion.playerRigidbody.position = TeleportPos;
        
        ExitState(context);
    }

    
}
