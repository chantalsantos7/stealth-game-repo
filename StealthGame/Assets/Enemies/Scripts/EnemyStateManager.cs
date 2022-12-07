using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Object References")]
    public EnemyManager enemyManager;
    public Transform player;
    public NavMeshAgent agent { get; private set; }
    public LayerMask playerMask, groundMask;

    [Header("Patrol Variables")]
    public bool patrolScheduled;
    public Transform[] waypoints;
    public float minIdleTimeInSeconds;
    public float maxIdleTimeInSeconds;
    public bool playerInSightRange;

    [Header("Attack Variables")]
    public float timeBetweenAttacks;
    public bool playerInAttackRange;

    //[Header("Interaction Ranges")]
    float sightRange;
    float attackRange;

    [HideInInspector]
    public EnemyState currentState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemySearchState suspiciousState = new EnemySearchState();


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerCharacter").transform;
        enemyManager = GetComponent<EnemyManager>();
        sightRange = enemyManager.detectionRadius;
        attackRange = enemyManager.attackRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        currentState = idleState; //switch to idle when patrol functionality complete
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
    }

    public void SwitchState(EnemyState state)
    {
        currentState.ExitState(this); 
        currentState = state;
        state.EnterState(this);
    }
}
