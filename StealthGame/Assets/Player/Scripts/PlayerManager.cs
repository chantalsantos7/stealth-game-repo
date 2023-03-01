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
    public float currentStamina;
    public float staminaRegenCooldown = 5f;
    public float CurrentStamina { get; private set; }

    public bool InStealth { get; set; }

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        //cameraManager = FindObjectOfType<CameraManager>();
        animator = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        CurrentStamina = maxStamina;
        //InStealth = false;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("IsInteracting");
        playerLocomotion.IsJumping = animator.GetBool("IsJumping");
        animator.SetBool("IsGrounded", playerLocomotion.isGrounded);
        
    }  
}
