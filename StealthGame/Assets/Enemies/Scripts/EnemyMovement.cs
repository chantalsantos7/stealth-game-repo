using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;

    private Vector2 velocity;
    private Vector2 smoothDeltaPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        SynchroniseAnimatorAndAgent();
    }

    private void SynchroniseAnimatorAndAgent()
    {
        //used to decide motion for enemy in blend tree, walking or running - could poss. be added to AnimatorManager to consolidate scripts
        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.1f);
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }
}
