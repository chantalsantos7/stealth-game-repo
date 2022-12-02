using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    //contains main enemy movement
    Transform[] waypoints;
    int waypointIndex = 0;
    Vector3 target = new();
    NavMeshAgent agent;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        waypoints = context.waypoints;
        UpdateDestination();
    }

    public override void UpdateState(EnemyStateManager context)
    {
        if (Vector3.Distance(agent.transform.position, target) < 1)
        {
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
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        throw new System.NotImplementedException();
    }

   
}
