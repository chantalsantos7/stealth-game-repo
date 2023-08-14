using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    Transform[] waypoints;
    public LayerMask groundMask;
    int waypointIndex = 0;
    Vector3 target = new();
    NavMeshAgent agent;
    float idleTimeInSeconds;
    float idleTimeElapsed;
    float minIdleTimeInSeconds;
    float maxIdleTimeInSeconds;

    public override void EnterState(EnemyStateManager context)
    {
        agent = context.agent;
        waypoints = context.waypoints;
        groundMask = context.groundMask;
        idleTimeElapsed = -5;
        minIdleTimeInSeconds = context.minIdleTimeInSeconds;
        maxIdleTimeInSeconds = context.maxIdleTimeInSeconds;
        
        //if enemy is armed from the search or attack states, disable the weapon models
        if (!context.enemyManager.isUnarmed)
        {
            context.enemyInventory.Disarm();
        }
        
        UpdateDestination();
    }

    public override void UpdateState(EnemyStateManager context)
    {
        idleTimeElapsed += Time.deltaTime;
        
        if (context.detectionSystem.canSeePlayer && !context.detectionSystem.inAttackRange)
        {
            context.SwitchState(context.chaseState);
        } 
        else if (context.detectionSystem.canSeePlayer && context.detectionSystem.inAttackRange)
        {
            context.SwitchState(context.attackState);
        } 
        else if (context.suspicionSystem.suspicionMeter > context.suspicionSystem.searchSuspicionThreshold)
        {
            context.SwitchState(context.searchState);
        }

        Vector3 distanceToWalkPoint = agent.transform.position - target;


        if (distanceToWalkPoint.magnitude < 1f && idleTimeElapsed > idleTimeInSeconds)
        {
            idleTimeElapsed = -10; //gives the agent time to move to the next patrol point, so they will idle longer
            IterateWayPointIndex();
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        SelectIdleTimeAtDestination();
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

    private void SelectIdleTimeAtDestination() //Randomly selects how long the enemy should idle at a patrol point
    {
        idleTimeInSeconds = Random.Range(minIdleTimeInSeconds, maxIdleTimeInSeconds);
    }

    public override void ExitState(EnemyStateManager context)
    {
        //throw new System.NotImplementedException();
    }
}
