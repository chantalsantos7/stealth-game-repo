using Assets.Scripts.Enumerators;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;
    InputManager inputManager;
    CameraManager cameraManager;
    
    Transform cameraObject;
    public Rigidbody playerRigidbody;
    CapsuleCollider playerCollider;

    //public Transform combatLookAt;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float maxDistance = 0.5f; //if using floating weaponCollider for stairs, change this and rayCastHeightOffset to 0.8f
    public LayerMask groundLayers;
    public float raycastHeightOffset = 0.5f;

    [Header("MovementFlags")]
    public bool isGrounded;
    public bool canMove;
    public bool IsJumping { get; set; }
    public bool IsSprinting { get; set; }
    public bool IsCrouched { get; set; }
    public bool IsMoving { get; set; }


    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -9.81f;

    [Header("Movement Speeds")]
    public float walkingSpeed = 3f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15;

    [Header("Drag")]
    public float groundDrag;

    [Header("Step Climbing")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.4f;
    [SerializeField] float stepSmooth = 0.1f;

    public Vector3 movementVelocity;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        cameraManager = Camera.main.GetComponent<CameraManager>();
        playerCollider = GetComponent<CapsuleCollider>();

        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
        isGrounded = true;
        canMove = true;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting) return;
        HandleMovementAndRotation();
        //HandleStairs();
        //HandleJumping();
    }

    private void HandleMovementAndRotation()
    {
        if (IsJumping || !canMove) return;

        //movement in the direction that the camera is facing
        IsMoving = inputManager.horizontalInput != 0 || inputManager.verticalInput != 0;
        movementVelocity = Vector3.zero;
        movementVelocity = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        movementVelocity += cameraObject.right * inputManager.horizontalInput;
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

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;

        //give the player drag while its on the ground, so they're not just sliding around, but remove it when they're in the air
        playerRigidbody.drag = isGrounded ? groundDrag : 0;

        if (IsCrouched)
        { 
            playerCollider.height = 1;
            playerCollider.center = new Vector3(0, 0.7f, 0);
            cameraManager.cameraMode = CameraMode.Crouch;
        }
        else
        {
            playerCollider.height = 1.71f;
            playerCollider.center = new Vector3(0, 1.03f, 0);
            cameraManager.cameraMode = CameraMode.Basic;
        }
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        raycastOrigin.y += raycastHeightOffset; //offset the height of the raycast so player does not fall through the world
        if (!isGrounded)
        {
            if (!playerManager.isInteracting) //HERE I need to not play if climbing down stairs
            {
                //playerAnimatorManager.PlayTargetAnimation("MidAir", true);
            }
            inAirTimer += Time.deltaTime;
            playerRigidbody.drag = 0;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(fallingVelocity * inAirTimer * -Vector3.up);
        }

        //Ground Checking
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayers))
        {
            Vector3 heightDiff = raycastOrigin - hit.point;
            //Debug.Log("Height to ground: " + heightDiff.y);
            if (!isGrounded && playerManager.isInteracting && heightDiff.y > 0.6f) //how to detect when going down stairs?????
            {
                playerAnimatorManager.PlayTargetAnimation("JumpLanding", true);
            }
            var rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !IsJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition; 
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded && !IsCrouched)
        {
            playerAnimatorManager.animator.SetBool("IsJumping", true); 
            playerAnimatorManager.PlayTargetAnimation("BasicMotions@Jump01", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * 9.81f * jumpHeight);
            Vector3 playerVelocity = movementVelocity;
            playerVelocity.y = 0f;
            playerRigidbody.AddForce(jumpingVelocity * Vector3.up, ForceMode.Impulse);
        }
    }  
    
    public void HandleDodging()
    {
        if (playerManager.isInteracting) return;
        //animatorManager.PlayTargetAnimation("Dodge", true, true);
        playerRigidbody.drag = 0;
        //dodge always moves back
        Vector3 dodgeForce = -transform.forward * 200f * Time.deltaTime;
        playerAnimatorManager.PlayTargetAnimation("Dodge", true);
        playerRigidbody.AddForce(dodgeForce, ForceMode.Impulse);
        //toggle invulnerability bool so dodge prevents damage
       
    }
}
