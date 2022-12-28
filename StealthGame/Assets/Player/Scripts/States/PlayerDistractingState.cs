using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistractingState : PlayerAbilitiesState
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public GameObject distractAim;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        //allow player to still move while in the placing view
        //enable the distractor aimer
        context.cameraManager.cameraMode = CameraMode.AimDistractor;
        distractAim = context.distractAim;
        distractAim.SetActive(true);
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        //placing aimer via camera view - aimer sort of attached to the camera view, moves where the camera is pointing

        Vector3 movementVelocity = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        movementVelocity += context.cameraManager.transform.right * context.inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        context.distractRigidbody.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = context.distractRigidbody.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(context.distractRigidbody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        context.distractRigidbody.transform.rotation = playerRotation;
    }

    public void DeployAbility()
    {

    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        context.cameraManager.cameraMode = CameraMode.Basic;
        distractAim.SetActive(false);
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }
}
