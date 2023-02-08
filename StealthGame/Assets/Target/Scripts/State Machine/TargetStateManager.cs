using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetStateManager : MonoBehaviour
{
    [Header("Patrol Variables")]
    public Transform[] waypoints;
    [Tooltip("Minimum amount of time the enemy should idle at a patrol point.")]
    public float minIdleTimeInSeconds;
    [Tooltip("Maximum amount of time the enemy should idle at a patrol point.")]
    public float maxIdleTimeInSeconds;
    // public bool playerInSightRange;

    public TargetState currentState;
    public TargetDeathState deathState = new TargetDeathState();
    public TargetMovementState moveState = new TargetMovementState();
    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = moveState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TargetState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
