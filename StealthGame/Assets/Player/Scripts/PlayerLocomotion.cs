using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    CameraManager cameraManager;
    Vector3 movementVelocity;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    CapsuleCollider playerCollider;

    //public Transform combatLookAt;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float maxDistance = 0.5f;
    public LayerMask groundLayers;
    public float raycastHeightOffset = 0.5f;

    [Header("MovementFlags")]
    public bool isGrounded;
    
    public bool isJumping { get; set; }
    public bool IsSprinting { get; set; }
    public bool IsCrouched { get; set; }


    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -9.81f;

    [Header("Movement Speeds")]
    public float walkingSpeed = 3f;
   // public float walkingSpeed = 2f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15;

    [Header("Drag")]
    public float groundDrag;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        cameraManager = Camera.main.GetComponent<CameraManager>();
        isGrounded = true;
        playerCollider = GetComponent<CapsuleCollider>();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting) return;
        HandleMovement();
        HandleRotation();
        //HandleJumping();
    }

    private void HandleMovement()
    {
        if (isJumping) return;

        //movement in the direction that the camera is facing
        movementVelocity = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        movementVelocity = movementVelocity + cameraObject.right * inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;
        
        //change movement speed depending on if player is walking or sprinting
        if (IsSprinting)
        {
            movementVelocity *= sprintingSpeed;
        } else
        {
            movementVelocity *= walkingSpeed;
        }

        playerRigidbody.velocity = movementVelocity;

        //give the player drag while its on the ground, so they're not just sliding around, but remove it when they're in the air
        playerRigidbody.drag = isGrounded ? groundDrag : 0;

        if (IsCrouched)
        {
            //change height and center of collider
            //OR
            //swap out for a new collider when crouched
            playerCollider.height = 1;
            playerCollider.center = new Vector3(0, 0.5f, 0);

            //playerCollider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        } else
        {
            playerCollider.height = 1.85f;
            playerCollider.center = new Vector3(0, 0.9f, 0);
        }
        /*
        if (isGrounded)
        {
            playerRigidbody.drag = groundDrag;
        } 
        else if (!isGrounded)
        {
            playerRigidbody.drag = 0;
        }*/
    }

    private void HandleRotation()
    {
        if (isJumping) return;
        //Vector3 targetDirection = Vector3.zero;
        //targetDirection = 
        //transform.rotation *= Quaternion.AngleAxis(inputManager.horizontalInput * rotationSpeed, Vector3.up);
        // Vector3 viewDirection = transform.position - new Vector3(transform.position)

        Vector3 targetDirection;
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
        //Vector3 targetPosition = transform.position;
        raycastOrigin.y += raycastHeightOffset;
        if (!isGrounded)
        {
            if (!playerManager.isInteracting && !isJumping)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            // animatorManager.PlayTargetAnimation("Falling", true);
            //BUG: Not actually handling the fall from jump, so gravity not being applied to jump
            inAirTimer += Time.deltaTime;
            playerRigidbody.drag = 0;
            if (!isJumping) playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(fallingVelocity * inAirTimer * -Vector3.up);
        }

        //sends a sphereCast directly down to check if player has hit the ground
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayers))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                //animatorManager.PlayTargetAnimation("Landing", true);
            }
            /*Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;*/
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

    private void HandleStairs()
    {
        //Send out raycasts in front of the player to check if there is a collider in front
        //starting from bottom, send at regular intervals until one ray passes a minimum stair depth
    }

    public void HandleJumping()
    {
        if (isGrounded && !IsCrouched)
        {
            animatorManager.animator.SetBool("IsJumping", true); 
            animatorManager.PlayTargetAnimation("Jumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = movementVelocity;
            playerVelocity.y = 0f;
            playerRigidbody.AddForce(jumpingVelocity * Vector3.up, ForceMode.Impulse);
            /*Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;*/
        }
    }    
}
