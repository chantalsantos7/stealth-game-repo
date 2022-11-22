using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportView : MonoBehaviour
{
    Transform cameraObject;
    InputManager inputManager;
    Rigidbody playerRigidbody;
    public GameObject mainPlayer;
    //get access to playerAbilities?

    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    public Vector3 teleportPosition { get; set; }

    private void Awake()
    {
        
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        //mainPlayer.SetActive(false);
        //deactivate the
        //third person controller?

    }

    void FixedUpdate()
    {
        HandleMovementAndRotation();
    }

    private void HandleMovementAndRotation()
    {
        Vector3 movementVelocity = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        movementVelocity += cameraObject.right * inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        playerRigidbody.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    //Player unable to jump/sprint/crouch in this mode
}
