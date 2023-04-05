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

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float maxDistance = 0.5f;
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
    public float teleportMovementSpeed;
    public float teleportRotationSpeed;

    [Header("Drag")]
    public float groundDrag;

    public Vector3 movementVelocity;
    Vector3 movementDirection;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        cameraManager = Camera.main.GetComponent<CameraManager>();
        playerCollider = GetComponent<CapsuleCollider>();

        isGrounded = true;
        canMove = true;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        isGrounded = GroundChecking();

        //handling drag
        playerRigidbody.drag = isGrounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        HandleAllMovement();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting) return;
        HandleMovementAndRotation();
    }

    private bool GroundChecking()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        raycastOrigin.y += raycastHeightOffset; //offset the height of the raycast so player does not fall through the world
        return Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out RaycastHit hit, maxDistance, groundLayers);
    }

    private void HandleMovementAndRotation()
    {
        if (IsJumping || !canMove) return;

        IsMoving = inputManager.horizontalInput != 0 || inputManager.verticalInput != 0;
        movementDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        movementDirection += cameraObject.right * inputManager.horizontalInput;
        movementDirection.Normalize();
        movementDirection.y = 0f;

        if (isGrounded)
        {
            if (IsSprinting)
            {
                movementDirection *= sprintingSpeed;
            }
            else
            {
                movementDirection *= walkingSpeed;
            }

            playerRigidbody.velocity = movementDirection;
        }

        if (movementDirection == Vector3.zero)
        {
            movementDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;

        if (IsCrouched)
        { 
            playerCollider.height = 1;
            playerCollider.center = new Vector3(0, 0.75f, 0);
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
        raycastOrigin.y += raycastHeightOffset;
        if (!isGrounded)
        {
            inAirTimer += Time.deltaTime;
            playerRigidbody.drag = 0;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(fallingVelocity * Vector3.down);
        }

        //Ground Checking
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayers))
        {
            Vector3 heightDiff = raycastOrigin - hit.point;
            if (!isGrounded && playerManager.isInteracting && heightDiff.y > 0.6f)
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
}
