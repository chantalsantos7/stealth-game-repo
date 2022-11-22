using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    PlayerAbilities playerAbilities;
    AnimatorManager animatorManager;
   
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprintModifierPressed;
    public bool crouchModifierPressed;
    public bool jumpKeyPressed;
    public bool teleportModifierPressed;
    public bool cancelTeleportKeyPressed;

    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAbilities = GetComponent<PlayerAbilities>();
        //crouchModifierPressed = false;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Sprint.performed += i => sprintModifierPressed = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintModifierPressed = false;
            playerControls.PlayerActions.Jump.performed += i => jumpKeyPressed = true;
            playerControls.PlayerActions.AimTeleport.performed += i =>
            {
                teleportModifierPressed = !teleportModifierPressed;
                playerAbilities.AimTeleport(); //BUG: NullReferenceException: Object reference not set to an instance of an object, but the code operates correctly?
            };
            playerControls.PlayerActions.Teleport.performed += i =>
            {
                if (playerAbilities.teleportAiming)
                {
                    playerAbilities.Teleport();
                }
            };

            /*playerControls.PlayerActions.Teleport.performed += i => teleportModifierPressed = true;
            playerControls.PlayerActions.Teleport.canceled += i => teleportModifierPressed = false;*/
            playerControls.PlayerActions.CancelTeleport.performed += i => cancelTeleportKeyPressed = true;
            playerControls.PlayerActions.Dodge.performed += context =>
            {
                if (context.interaction is MultiTapInteraction)
                {
                    playerLocomotion.HandleDodging();
                }
            };
            //playerControls.PlayerActions.Crouch.triggered += i => crouchModifierPressed = true ? crouchModifierPressed = false : crouchModifierPressed = true;
            //even if main teleport button is being pressed, the cancel button should disable the teleport entirely
            //if cancel button not pressed, teleport happens when button is released
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {   
        HandleCrouchInput();
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        //HandleTeleportInput();
        //Debug.Log(crouchModifierPressed);
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.IsSprinting, playerLocomotion.IsCrouched);
    }

    private void HandleSprintingInput()
    {
        if (sprintModifierPressed && moveAmount > 0.5f)
        {
            playerLocomotion.IsSprinting = true;
        } 
        else
        {
            playerLocomotion.IsSprinting = false;
        }
    }

    private void HandleTeleportInput()
    {
        //reverse the aimingMode bool like the crouched bool
        //set the 
    }

    private void HandleCrouchInput()
    {
        if (playerControls.PlayerActions.Crouch.triggered)
        {
            playerLocomotion.IsCrouched = !playerLocomotion.IsCrouched;
            crouchModifierPressed = !crouchModifierPressed;
        }
    }

    public void HandleJumpInput()
    {
        if (jumpKeyPressed)
        {
            playerLocomotion.HandleJumping();
            jumpKeyPressed = false;
        }
    }
}
