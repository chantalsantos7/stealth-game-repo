using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    //contains main enemy movement
    Transform[] waypoints;
    Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;
    public LayerMask groundMask;
    int waypointIndex = 0;
    Vector3 target = new();
    NavMeshAgent agent;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        waypoints = context.waypoints;
        walkPointRange = context.walkPointRange;
        groundMask = context.groundMask;    
        UpdateDestination();
        //Debug.Log("first target is: " + target);
    }

    public override void UpdateState(EnemyStateManager context)
    {
        if (context.playerInSightRange && !context.playerInAttackRange)
        {
            context.SwitchState(context.chaseState);
        } 
        else if (context.playerInSightRange && context.playerInAttackRange)
        {
            context.SwitchState(context.attackState);
        }

        Vector3 distanceToWalkPoint = agent.transform.position - target;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log(agent.pathStatus);
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWayPointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length) 
        {
            waypointIndex = 0;
        }
    }

    public override void ExitState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        //throw new System.NotImplementedException();
    }

   
}
