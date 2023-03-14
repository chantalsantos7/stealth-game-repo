using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    int crouched;
    int horizontal;
    int vertical;

    protected override void Awake()
    {
        base.Awake();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        crouched = Animator.StringToHash("Crouched");
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting, bool isCrouched)
    {
        if (!playerLocomotion.canMove) return;
        float snappedHorizontal = SnapMovementValue(horizontalMovement, 0.55f);
        float snappedVertical = SnapMovementValue(verticalMovement, 0.55f);

        if (isSprinting)
        {
            snappedVertical = 2;
        }

        animator.SetBool(crouched, isCrouched);
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    private float SnapMovementValue(float movementValue, float threshold)
    {
        float snappedValue;

        if (movementValue > 0f && movementValue < threshold)
        {
            snappedValue = 0.5f;
        }
        else if (movementValue > threshold)
        {
            snappedValue = 1f;
        }
        else if (movementValue < 0f && movementValue > -threshold)
        {
            snappedValue = -0.5f;
        }
        else if (movementValue < -threshold)
        {
            snappedValue = -1f;
        }
        else
        {
            snappedValue = 0f;
        }

        return snappedValue;
    }
}
