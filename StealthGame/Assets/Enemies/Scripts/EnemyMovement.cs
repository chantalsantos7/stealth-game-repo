using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;

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
        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.1f);
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }
}
