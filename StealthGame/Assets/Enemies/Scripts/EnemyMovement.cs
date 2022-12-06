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

    // Start is called before the first frame update
    void Start()
    {
        
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
        agent.nextPosition = rootPosition;*/
    }

    private void SynchroniseAnimatorAndAgent()
    {
        //BUG: He just be zooming all over the place
/*        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        velocity = smoothDeltaPosition / Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            velocity = Vector2.Lerp(Vector2.zero, velocity, agent.remainingDistance / agent.stoppingDistance);
        }*/

        /*bool shouldMove = velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance; //TODO: make this (velocity mag to check) a serializable property to play w/ it

        animator.SetBool("IsMoving", shouldMove);
        animator.SetFloat("Velocity", velocity.magnitude);*/

       /* float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(animator.rootPosition, agent.nextPosition, smooth);
        }*/
    }
}
