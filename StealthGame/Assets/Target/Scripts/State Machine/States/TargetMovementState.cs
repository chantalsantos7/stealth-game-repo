using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMovementState : TargetState
{
    Transform[] waypoints;
    NavMeshAgent agent;
    int waypointIndex = 0;
    Vector3 target = new();
    float idleTimeElapsed;
    float idleTimeInSeconds;
    float minIdleTimeInSeconds;
    float maxIdleTimeInSeconds;
    Vector3 distanceToWalkPoint;
    
    public override void EnterState(TargetStateManager context)
    {
        agent = context.agent;
        waypoints = context.waypoints;
        idleTimeElapsed = -5;
        minIdleTimeInSeconds = context.minIdleTimeInSeconds;
        maxIdleTimeInSeconds = context.maxIdleTimeInSeconds;
        UpdateDestination();
    }

    public override void UpdateState(TargetStateManager context)
    {
        idleTimeElapsed += Time.deltaTime;

        distanceToWalkPoint = agent.transform.position - target;
        if (distanceToWalkPoint.magnitude < 1f && idleTimeElapsed > idleTimeInSeconds)
        {
            //Debug.Log("Time elapsed: " + idleTimeElapsed);
            //Debug.Log(agent.pathStatus);
            idleTimeElapsed = 0; //gives the agent time to move to the next patrol point, so they will idle longer
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    public override void ExitState(TargetStateManager context)
    {
        //throw new System.NotImplementedException();
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        CalculateIdleTime();
        agent.SetDestination(target);
    }

    private void IterateWayPointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void CalculateIdleTime()
    {
        idleTimeInSeconds = Random.Range(minIdleTimeInSeconds, maxIdleTimeInSeconds);
    }

    public override void OnCollisionEnter(TargetStateManager context, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
