using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float maxDistance = 0.5f;
    public LayerMask groundLayers;
    public float raycastHeightOffset = 0.5f;

    [Header("MovementFlags")]
    public bool isGrounded;
    public bool IsSprinting { get; set; }
    public bool IsCrouched { get; set; }


    [Header("Movement Speeds")]
    public float walkingSpeed = 3f;
   // public float walkingSpeed = 2f;
    public float sprintingSpeed = 7f;

    public float rotationSpeed = 15;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting) return;
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0f;
        
        //change movement speed depending on if player is walking or sprinting
        if (IsSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        } else
        {
            moveDirection = moveDirection * walkingSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;

    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += raycastHeightOffset;
        if (!isGrounded)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }
            animatorManager.PlayTargetAnimation("Falling", true);
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        //sends a sphereCast directly down to check if player has hit the grounf
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayers))
        {
            if (!isGrounded &&  playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    
}
