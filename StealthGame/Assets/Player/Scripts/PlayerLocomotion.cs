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
   // public float walkingSpeed = 2f;
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
        //HandleRotation();
        HandleStairs();
        //HandleJumping();
    }

    private void HandleMovementAndRotation()
    {
        if (IsJumping || !canMove) return;

        //movement in the direction that the camera is facing
        IsMoving = inputManager.horizontalInput != 0 || inputManager.verticalInput != 0 ? true : false;
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

        //Debug.Log("moving velocity is: " + playerRigidbody.velocity);

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;

        //give the player drag while its on the ground, so they're not just sliding around, but remove it when they're in the air
        playerRigidbody.drag = isGrounded ? groundDrag : 0;

        if (IsCrouched)
        {
            //change height and center of weaponCollider
            //OR
            //swap out for a new weaponCollider when crouched
            playerCollider.height = 1;
            playerCollider.center = new Vector3(0, 0.5f, 0);

            //playerCollider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            playerCollider.height = 1.85f;
            playerCollider.center = new Vector3(0, 0.92f, 0);
        }
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        //Vector3 targetPosition = transform.position;
        raycastOrigin.y += raycastHeightOffset; //offset the height of the raycast so player does not fall through the world
        if (!isGrounded)
        {
            if (!playerManager.isInteracting && !IsJumping) //TODO: Add more checks when climbing stairs so animation isn't triggered - raycast distance, if smaller than jump height don't fall?
            {
                playerAnimatorManager.PlayTargetAnimation("Falling", true);
            }

            // animatorManager.PlayTargetAnimation("Falling", true);
            //BUG: Not actually handling the fall from jump, so gravity not being applied to jump
            inAirTimer += Time.deltaTime;
            playerRigidbody.drag = 0;
            if (!IsJumping) playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(fallingVelocity * inAirTimer * -Vector3.up);
        }

        //sends a sphereCast directly down to check if player has hit the ground
        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayers))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                //animatorManager.PlayTargetAnimation("Landing", true);
            }
           /* Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;*/
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        //Floating weaponCollider stairs code
        /*if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }*/

    }

    private void HandleStairs()
    {
        //Send out raycasts in front of the player to check if there is a weaponCollider in front
        //starting from bottom, send at regular intervals until one ray passes a minimum stair depth
        Vector3[] rayDirections = new Vector3[]
        {
            new Vector3(0f, 0f, 1f),
            new Vector3(1f, 0f, 1f),
            new Vector3(-1f, 0f, 1f)
        };

        if (inputManager.moveAmount > 0)
        {
            foreach (Vector3 direction in rayDirections)
            {
                Debug.DrawRay(stepRayLower.transform.position, transform.TransformDirection(direction), Color.green);
                Debug.DrawRay(stepRayUpper.transform.position, transform.TransformDirection(direction), Color.red);
                if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(direction), 0.2f))
                {
                    if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(direction), 0.3f))
                    {
                        playerRigidbody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
                    }
                }
            }
        }

        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y += raycastHeightOffset; //offset the height of the raycast so player does not fall through the world
        RaycastHit stairsHit;
        //handle going down stairs
        if (Physics.Raycast(raycastOrigin, Vector3.down, out stairsHit, 0.5f))
        {
            Debug.DrawRay(raycastOrigin, Vector3.down, Color.magenta);
            Vector3 heightDiff = raycastOrigin - stairsHit.point;
            if (heightDiff.y < jumpHeight)
            {
                //apply gravity/downward force here so player actually will go down
                
                
            }
            //find the difference between the original position and the ray hit
            //if less than stair height don't play falling animation
        }
    }

    public void HandleJumping()
    {
        if (isGrounded && !IsCrouched)
        {
            playerAnimatorManager.animator.SetBool("IsJumping", true); 
            playerAnimatorManager.PlayTargetAnimation("Jumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = movementVelocity;
            playerVelocity.y = 0f;
            playerRigidbody.AddForce(jumpingVelocity * Vector3.up, ForceMode.Impulse);
            /*Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;*/
        }
    }  
    
    public void HandleDodging()
    {
        if (playerManager.isInteracting) return;
        //animatorManager.PlayTargetAnimation("Dodge", true, true);
        playerRigidbody.drag = 0;
        //dodge always moves back
        Vector3 dodgeForce = -transform.forward * 2000f * Time.deltaTime;
        playerAnimatorManager.PlayTargetAnimation("Dodge", true);
        playerRigidbody.AddForce(dodgeForce, ForceMode.Impulse);
        //toggle invulnerability bool so dodge prevents damage
       
    }
}
