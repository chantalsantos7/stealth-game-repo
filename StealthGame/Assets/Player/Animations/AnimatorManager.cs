using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Animation Snapping
        float snappedHorizontal = SnapMovementValue(horizontalMovement, 0.55f);
        float snappedVertical = SnapMovementValue(verticalMovement, 0.55f);

        if (isSprinting)
        {
            snappedHorizontal = SnapMovementValue(horizontalMovement, 0.55f);
            snappedVertical = 2;

        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat (vertical, snappedVertical, 0.1f, Time.deltaTime);
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
