using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [HideInInspector] public EnemyManager enemyManager;
    [HideInInspector] public EnemyAnimatorManager enemyAnimatorManager;
    [HideInInspector] public DetectionSystem detectionSystem;
    [HideInInspector] public SuspicionSystem suspicionSystem;
    [HideInInspector] public EnemyInventory enemyInventory;
    [HideInInspector] public GameObject player;
    [HideInInspector] public SFB_AudioManager audioManager;
    public NavMeshAgent agent { get; private set; }

    [Header("Object References")]
    public GameObject swordSheathe;

    public LayerMask playerMask, groundMask;

    [Header("Patrol Variables")]
    public bool patrolScheduled;
    public Transform[] waypoints;
    [Tooltip("Minimum amount of time the enemy should idle at a patrol point.")]
    public float minIdleTimeInSeconds;
    [Tooltip("Maximum amount of time the enemy should idle at a patrol point.")]
    public float maxIdleTimeInSeconds;
    public bool playerInSightRange;

    [Header("Attack Variables")]
    public float timeBetweenAttacks;
    public bool inAttackState;

    [HideInInspector] public float searchSuspicionThreshold;
    [HideInInspector] public float chaseSuspicionThreshold;
    [HideInInspector] public float patrolSuspicionThreshold;

    public EnemyState currentState;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemySearchState suspiciousState = new();
    public EnemyDeathState deathState = new();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        detectionSystem = GetComponent<DetectionSystem>();
        suspicionSystem = GetComponent<SuspicionSystem>();
        enemyInventory = GetComponent<EnemyInventory>();
        audioManager = GetComponentInChildren<SFB_AudioManager>();

        searchSuspicionThreshold = suspicionSystem.searchSuspicionThreshold;
        chaseSuspicionThreshold = suspicionSystem.chaseSuspicionThreshold;
        patrolSuspicionThreshold = suspicionSystem.patrolSuspicionThreshold;
    }

    void Start()
    {
        currentState = idleState; //switch to idle when patrol functionality complete
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        Debug.Log("Switching to " + state.ToString());
        currentState.ExitState(this); 
        currentState = state;
        state.EnterState(this);
    }
}
