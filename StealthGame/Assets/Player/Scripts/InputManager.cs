using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;
    PlayerCombat playerCombat;
    WeaponsInventory playerInventory;
    PlayerAbilitiesStateManager abilitiesManager;
    PlayerAnimatorManager playerAnimatorManager;
   
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private bool sprintModifierPressed;
    private bool crouchModifierPressed;
    private bool jumpKeyPressed;
    private bool teleportModifierPressed;
    private bool distractionModifierPressed;
    public bool TeleportKeyPressed { get; set; }
    public bool DistractKeyPressed { get; set; }
    public bool InteractKeyPressed { get; set; }

    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    private void Awake()
    {
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();
        playerInventory = GetComponent<WeaponsInventory>();
        playerCombat = GetComponent<PlayerCombat>();
        abilitiesManager = GetComponent<PlayerAbilitiesStateManager>();
        TeleportKeyPressed = false;
        //crouchModifierPressed = false;
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
            playerControls.UI.Interact.performed += i => InteractKeyPressed = true;
            playerControls.UI.Interact.canceled += i => InteractKeyPressed = false;
            playerControls.UI.ToggleObjectives.performed += i => GameManager.Instance.uiManager.ToggleObjectivesPanel();
            //playerControls.UI.

            playerControls.PlayerActions.AimTeleport.performed += i =>
            {
                //teleportModifierPressed = !teleportModifierPressed
                if (teleportModifierPressed)
                {
                    teleportModifierPressed = false;
                    abilitiesManager.SwitchState(abilitiesManager.baseState);
                }
                else if (abilitiesManager.teleportAllowed)
                {
                    //teleportModifierPressed = !teleportModifierPressed;
                    teleportModifierPressed = true;
                    abilitiesManager.SwitchState(abilitiesManager.teleportingState);
                } 
                else
                {
                    Debug.Log("Teleport not allowed yet");
                }
            };
            
            playerControls.PlayerActions.Teleport.performed += i =>
            {
                if (abilitiesManager.currentState == abilitiesManager.teleportingState)
                    TeleportKeyPressed = true;
            };

            playerControls.PlayerActions.PutDistraction.performed += i =>
            {
                if (abilitiesManager.currentState == abilitiesManager.distractingState) 
                    DistractKeyPressed = true;
            };

            playerControls.PlayerActions.AimDistraction.performed += i =>
            {
                if (distractionModifierPressed)
                {
                    distractionModifierPressed = false;
                    abilitiesManager.SwitchState(abilitiesManager.baseState);
                }
                else if (abilitiesManager.distractionAllowed)
                {
                     distractionModifierPressed = true;
                    Debug.Log("Can actually be reached");
                    abilitiesManager.SwitchState(abilitiesManager.distractingState);
                }
                else
                {
                    Debug.Log("Not yet allowed;");
                }
            };

            playerControls.PlayerActions.Dodge.performed += context =>
            {
                if (context.interaction is MultiTapInteraction)
                {
                    playerLocomotion.HandleDodging();
                }
            };

            playerControls.PlayerActions.LeftAttack.performed += i =>
            {
                //get the weapon on the left slot, rather than directly referencing the leftWeapon
                //change to just 'Attack' pick which weapon to attack w/ randomly
                if (playerCombat.allowedToAttack)
                {
                    if (playerManager.InStealth)
                    {
                        playerCombat.SneakAttack();
                    }
                    else
                    {
                        playerCombat.Attack();
                    }
                    
                }
                //random choose betwen left or right weapon
            };

            /*playerControls.PlayerActions.RightAttack.performed += i =>
            {
                playerCombat.Attack(playerInventory.rightWeapon, false);
            };*/

            playerControls.PlayerActions.SheatheUnsheathe.performed += i =>
            {
                playerCombat.isUnarmed = !playerCombat.isUnarmed;

                //Player takes out their weapon. If the player already has a weapon out, they will put the weapon away.
                if (playerCombat.isUnarmed) playerInventory.Disarm();
                else playerInventory.TakeOutWeapon();
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
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        playerAnimatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.IsSprinting, playerLocomotion.IsCrouched);
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
            playerManager.InStealth = !playerManager.InStealth;
            crouchModifierPressed = !crouchModifierPressed;
        }
    }

    private void HandleTeleportInput()
    {
        if (playerControls.PlayerActions.AimTeleport.triggered)
        {

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
