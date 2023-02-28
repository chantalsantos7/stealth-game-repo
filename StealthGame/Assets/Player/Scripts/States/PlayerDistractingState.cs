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
        previewPos = context.distractorPreview;
        previewPos.SetActive(true);
        distractor = context.distractorObj;
        GameManager.Instance.uiManager.ToggleDistractDeploy();
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
        GameManager.Instance.uiManager.ToggleDistractDeploy();
        GameManager.Instance.uiManager.SetDistractIconTransparency(0);
        //context.cameraManager.cameraMode = CameraMode.Basic;
        //previewPos.SetActive(false);
        //context.playerLocomotion.canMove = true;
    }

    public override void OnCollisionEnter(PlayerAbilitiesStateManager context, Collision collision)
    {
        
    }
}
