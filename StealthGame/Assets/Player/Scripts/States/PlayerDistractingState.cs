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
        //need to spawn the preview!!!
        //GameObject.Instantiate(previewPos, emitterPosition, Quaternion.identity);
        previewPos.SetActive(true);
        distractor = context.distractorObj;
    }

    public override void UpdateState(PlayerAbilitiesStateManager context)
    {

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
        GameManager.Instance.achievementTracker.DistractUsed++;
        context.SwitchState(context.baseState);
    }

    public override void ExitState(PlayerAbilitiesStateManager context)
    {
        previewPos.SetActive(false);
        context.distractionAllowed = false;
        //context.cameraManager.cameraMode = CameraMode.Basic;
        //previewPos.SetActive(false);
        //context.playerLocomotion.canMove = true;
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }
}
