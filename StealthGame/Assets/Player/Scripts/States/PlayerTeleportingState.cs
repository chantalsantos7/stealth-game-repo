using Assets.Scripts.Enumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingState : PlayerAbilitiesState
{
    float teleportRadiusLimit;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    Rigidbody teleportRb;
    //float teleportCooldown = 10f;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        Debug.Log("Teleport mode entered");
        context.cameraManager.cameraMode = CameraMode.AimTeleport;
        context.teleportView.transform.position = context.transform.position + Vector3.right;
        context.playerLocomotion.canMove = false;
        context.teleportView.SetActive(true);
        teleportRadiusLimit = context.player.teleportLimit;
        teleportRb = context.teleportRigidbody;
        GameManager.Instance.uiManager.ToggleTeleportDeploy();
        //GameManager.Instance.uiManager.SetTeleportIconTransparency(100);
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
        Vector3 centerPosition = context.playerLocomotion.playerRigidbody.position;
        var distance = Vector3.Distance(teleportRb.position, centerPosition);

        /*if (distance > teleportRadiusLimit)
        {
            Vector3 fromOrigin = context.teleportRigidbody.position - centerPosition;
            fromOrigin *= teleportRadiusLimit / distance;
            var newPosition = centerPosition + fromOrigin;
            context.teleportRigidbody.position = newPosition;
            return;
        }*/

        /*if (distance > teleportRadiusLimit)
        {
            Debug.Log("Too far!"); //replace with a UI showing the distance you can travel
            teleportRb.position -= new Vector3(0, 0, 0.2f); //move it back slightly, so it doesn't get stuck
            return;
        }*/
        
        Vector3 movementVelocity = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        movementVelocity += context.cameraManager.transform.right * context.inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        teleportRb.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = teleportRb.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(teleportRb.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        teleportRb.transform.rotation = playerRotation;

        Vector3 raycastOrigin = teleportRb.transform.position;
        //Vector3 targetPosition = transform.position;
        //raycastOrigin.y += raycastHeightOffset; //offset the height of the raycast so player does not fall through the world
        
        if (!Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out RaycastHit hit, 
            context.playerLocomotion.maxDistance, context.playerLocomotion.groundLayers))
        {
            teleportRb.AddForce(context.playerLocomotion.fallingVelocity * Vector3.down);
            
        }
        
        if (context.inputManager.TeleportKeyPressed)
        {
            Teleport(context);
            context.inputManager.TeleportKeyPressed = false;
        }
    }

    private void Teleport(PlayerAbilitiesStateManager context)
    {
        Vector3 TeleportPos = context.teleportView.transform.position;
        context.playerLocomotion.playerRigidbody.position = TeleportPos;
        GameManager.Instance.achievementTracker.TeleportUsed++;
        context.audioSource.PlayOneShot(context.teleportSoundEffect);
        context.SwitchState(context.baseState);
    }

    
}
