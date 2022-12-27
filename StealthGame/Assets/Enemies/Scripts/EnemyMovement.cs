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

        /*animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;*/
    }

    // Update is called once per frame
    void Update()
    {
        SynchroniseAnimatorAndAgent();
    }

    private void OnAnimatorMove()
    {
        /*Vector3 rootPosition = animator.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;*/ //set the agent to move based on position of model
    }

    private void SynchroniseAnimatorAndAgent()
    {
        animator.SetBool("IsMoving", agent.velocity.magnitude > 0.1f);
        animator.SetFloat("Velocity", agent.velocity.magnitude);
        Debug.Log("Agent Velocity: " + agent.velocity.magnitude);
        
    }
}
