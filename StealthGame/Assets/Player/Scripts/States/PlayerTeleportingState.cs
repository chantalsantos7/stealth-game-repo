using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingState : PlayerAbilitiesState
{
    float teleportRadiusLimit;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    public override void EnterState(PlayerAbilitiesStateManager stateManager)
    {
        Debug.Log("Teleport mode entered");
        context = stateManager;
        context.cameraManager.cameraMode = CameraMode.AimTeleport;
        context.teleportView.transform.position = context.transform.position + Vector3.right;
        context.playerLocomotion.canMove = false;
        context.teleportView.SetActive(true);
        teleportRadiusLimit = context.player.teleportLimit;
        //movementSpeed = 
    }

    public override void ExitState()
    {
        context.cameraManager.cameraMode = CameraMode.Basic;
        context.teleportView.SetActive(false);
        context.playerLocomotion.canMove = true;
        context.teleportParticles.Stop();
        //stateManager.teleportParticles.gameObject.SetActive(false);
        context.SwitchState(context.baseState);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(context.teleportRigidbody.position, context.playerLocomotion.playerRigidbody.position) > teleportRadiusLimit)
        {
            Debug.Log("Too far!"); //replace with a UI showing the distance you can travel
            context.teleportRigidbody.position -= new Vector3(0, 0, 0.5f); //move it back slightly, so it doesn't get stuck
            return;
        }
        Debug.Log("Teleport state update being called");
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
            Teleport();
            context.inputManager.teleportKeyPressed = false;
        }

    }

    private void Teleport()
    {
        //stateManager.teleportParticles.gameObject.SetActive(true);
        context.teleportParticles.Emit(1);
        Vector3 TeleportPos = context.teleportView.transform.position;
        //stateManager.teleportView.SetActive(false);
        context.playerLocomotion.playerRigidbody.position = TeleportPos;
        ExitState();
    }
}
