using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
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
        animator = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        CurrentStamina = maxStamina;
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("IsInteracting");
        playerLocomotion.IsJumping = animator.GetBool("IsJumping");
        animator.SetBool("IsGrounded", playerLocomotion.isGrounded);
    }  
}
