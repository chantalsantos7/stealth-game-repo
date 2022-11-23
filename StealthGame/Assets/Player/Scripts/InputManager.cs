using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    PlayerAbilities playerAbilities;
    PlayerCombat playerCombat;
    PlayerInventory playerInventory;
    AnimatorManager animatorManager;
   
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private bool sprintModifierPressed;
    private bool crouchModifierPressed;
    private bool jumpKeyPressed;
    private bool teleportModifierPressed;

    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerInventory = GetComponent<PlayerInventory>();
        playerCombat = GetComponent<PlayerCombat>();
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
                playerAbilities.AimTeleport();
            };
            playerControls.PlayerActions.Teleport.performed += i =>
            {
                if (playerAbilities.teleportAiming)
                {
                    playerAbilities.Teleport();
                }
            };

            playerControls.PlayerActions.Dodge.performed += context =>
            {
                if (context.interaction is MultiTapInteraction)
                {
                    playerLocomotion.HandleDodging();
                }
            };

            playerControls.PlayerActions.Attack.performed += i =>
            {
                playerCombat.Attack(playerInventory.leftWeapon);
            };
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
