using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    //contains main enemy movement
    Transform[] waypoints;
    public LayerMask groundMask;
    int waypointIndex = 0;
    Vector3 target = new();
    NavMeshAgent agent;
    float idleTimeInSeconds = 20;
    float idleTimeElapsed;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        waypoints = context.waypoints;
        groundMask = context.groundMask;
        idleTimeElapsed = 0;
        UpdateDestination();
        //Debug.Log("first target is: " + target);
    }

    public override void UpdateState(EnemyStateManager context)
    {
        idleTimeElapsed += Time.deltaTime;
        if (context.playerInSightRange && !context.playerInAttackRange)
        {
            context.SwitchState(context.chaseState);
        } 
        else if (context.playerInSightRange && context.playerInAttackRange)
        {
            context.SwitchState(context.attackState);
        }

        Vector3 distanceToWalkPoint = agent.transform.position - target;
        if (distanceToWalkPoint.magnitude < 1f && idleTimeElapsed > idleTimeInSeconds)
        {
            Debug.Log(idleTimeElapsed);
            Debug.Log(agent.pathStatus);
            idleTimeElapsed = 0;
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
