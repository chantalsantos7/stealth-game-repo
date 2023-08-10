using UnityEngine;
using UnityEngine.AI;

public class EnemySearchState : EnemyState
{
    NavMeshAgent agent;
    SuspicionSystem suspicionSystem;
    DetectionSystem detectionSystem;
    
    float patrolThreshold;
    float chaseThreshold;

    private Vector3 target;

    private int searchPoints = 4;
    private int pointsSearched = 0;
    
    public override void EnterState(EnemyStateManager context)
    {
        GameManager.Instance.EnemyIsSuspicious = true; //will activate suspicion indicator
        agent = context.agent;
        suspicionSystem = context.suspicionSystem;
        detectionSystem = context.detectionSystem;

        chaseThreshold = context.chaseSuspicionThreshold;
        patrolThreshold = context.patrolSuspicionThreshold;
        context.audioManager.PlayAudio("Suspicion");
        context.enemyInventory.TakeOutWeapon();

        target = detectionSystem.lastKnownPosition;
        agent.SetDestination(target);

    }
    
    public override void UpdateState(EnemyStateManager context)
    {
        
        //after it reaches the last known position, move around randomly until suspicion meter is down
        Vector3 distanceToWalkPoint = agent.transform.position - target;

        //walk to a new position
        if (distanceToWalkPoint.magnitude < 0.5f && pointsSearched < searchPoints)
        {
            GenerateNewTargetSearchPosition();
            pointsSearched++;
            Debug.Log(pointsSearched);
            agent.SetDestination(target);
        }
        /*else if (pointsSearched >= searchPoints && )
        {
            context.SwitchState(context.patrolState);
        }*/

        if (detectionSystem.canSeePlayer && suspicionSystem.suspicionMeter > chaseThreshold)
        {
            context.SwitchState(context.chaseState); 
        }

        if (suspicionSystem.suspicionMeter < patrolThreshold && !detectionSystem.canSeePlayer && !detectionSystem.heardSomething)
        {
            context.SwitchState(context.patrolState);
        }

        if (detectionSystem.canSeePlayer && detectionSystem.inAttackRange)
        {
            context.SwitchState(context.attackState);
        }
    }

    //Select a random position slightly displaced from the original target position, that the enemy will walk to
    //problem now is making sure there isn't anything around that could stop enemy from moving on from that point
    //Sample the position on the navmesh to make sure it's valid, if not find a valid point on the navmesh (Ai.NavMesh.SamplePosition)
    //SamplePosition is working but probs causing memory problems w/ constant search, limit number of times a new walkPoint is generated
    //also see if can restrict to 1 NavmeshArea
    private void GenerateNewTargetSearchPosition()
    {
        
        Vector3 newTargetPosition = SelectNewXorZCoordForPosition(target, 1f, 5f);
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(newTargetPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            newTargetPosition = SelectNewXorZCoordForPosition(target, 4f, 10f);
        }
        target = hit.position;
        Debug.Log("target's new position is: " + target);
    }

    private Vector3 SelectNewXorZCoordForPosition(Vector3 originalPosition, float minInclusiveRange, float maxInclusiveRange)
    {
        float newTargetX = originalPosition.x + Random.Range(minInclusiveRange, maxInclusiveRange);
        float newTargetZ = originalPosition.z + Random.Range(minInclusiveRange, maxInclusiveRange);
        Vector3 newTargetPosition;
        int decider = Random.Range(1, 3);
        newTargetPosition = decider == 1 ? new Vector3(newTargetX, 0, originalPosition.z) : new Vector3(originalPosition.x, 0, newTargetZ);
        return newTargetPosition;
    }

    public override void ExitState(EnemyStateManager context)
    {
        GameManager.Instance.EnemyIsSuspicious = false;
    }

    public override void OnCollisionEnter(EnemyStateManager context, Collision other)
    {
        
    }

    
}
