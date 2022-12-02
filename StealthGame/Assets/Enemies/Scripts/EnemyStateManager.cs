using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{

    public Transform[] waypoints;
    public NavMeshAgent agent { get; private set; }

    public EnemyState currentState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemySearchState suspiciousState = new EnemySearchState();


    // Start is called before the first frame update
    void Start()
    { 
        currentState = patrolState; //switch to idle when patrol functionality complete
        agent = GetComponent<NavMeshAgent>();
        currentState.EnterState(this);
           
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void SwitchState(EnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
