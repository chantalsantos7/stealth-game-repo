using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;
    //CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    public bool isInteracting;
    public bool isUsingRootMotion;

    [Header("Player Stats")]
    public float maxStamina;
    public float maxHealth = 100f;
    public float currentStamina;
    public float staminaRegenCooldown = 5f;
    public float Health { get; private set; }
    public float CurrentStamina { get; private set; }
    [Tooltip("How far away the player can teleport from their initial position.")] public float teleportLimit = 10f;
    [Tooltip("Limits the distance that the distraction aimer can go from the player.")] public float distractAbilityLimit = 1f;
    
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        //cameraManager = FindObjectOfType<CameraManager>();
        animator = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        CurrentStamina = maxStamina;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("IsInteracting");
        playerLocomotion.IsJumping = animator.GetBool("IsJumping");
        animator.SetBool("IsGrounded", playerLocomotion.isGrounded);
        
    }  
}
