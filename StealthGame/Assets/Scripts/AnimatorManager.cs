using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("IsInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void SetBool(string boolVariable, bool value)
    {
        animator.SetBool(boolVariable, value);
    }

    
}
