using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistractingState : PlayerAbilitiesState
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    GameObject previewPos;
    GameObject distractor;
    //Rigidbody distractRigidbody;
    float distractRadiusLimit;

    public override void EnterState(PlayerAbilitiesStateManager context)
    {
        //allow player to still move while in the placing view
        //enable the distractor aimer
        /*        context.cameraManager.cameraMode = CameraMode.AimDistractor;
                context.playerLocomotion.canMove = false;

                distractRigidbody = previewPos.GetComponent<Rigidbody>();*/
        previewPos = context.distractorPreview;
        previewPos.SetActive(true);
        distractor = context.distractorObj;
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {
        //placing aimer via camera view - aimer sort of attached to the camera view, moves where the camera is pointing
        /*if (Vector3.Distance(distractRigidbody.position, context.playerLocomotion.transform.position) > distractRadiusLimit)
        {
            Debug.Log("Too far!"); //replace with a UI showing the distance you can travel
            distractRigidbody.position -= new Vector3(0, 0, 0.2f); //move it back slightly, so it doesn't get stuck
            return;
        }*/
/*
        Vector3 movementVelocity = new Vector3(context.cameraManager.transform.forward.x, 0f, context.cameraManager.transform.forward.z) * context.inputManager.verticalInput;
        movementVelocity += context.cameraManager.transform.right * context.inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        distractRigidbody.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = distractRigidbody.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(distractRigidbody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        distractRigidbody.transform.rotation = playerRotation;*/

        if (context.inputManager.DistractKeyPressed)
        {
            DeployAbility(context);
            context.inputManager.DistractKeyPressed = false;
        }
    }

    public void DeployAbility(PlayerAbilitiesStateManager context)
    {
        //spawn a gameObject that functions as a distraction
        Vector3 emitterPosition = new Vector3(previewPos.transform.position.x, 0, previewPos.transform.position.z);
        GameObject.Instantiate(distractor, emitterPosition, Quaternion.identity);

        context.SwitchState(context.baseState);
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        previewPos.SetActive(false);
        //context.cameraManager.cameraMode = CameraMode.Basic;
        //previewPos.SetActive(false);
        //context.playerLocomotion.canMove = true;
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }
}
