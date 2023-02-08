using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public Transform[] waypoints;
    NavMeshAgent agent;
    int waypointIndex = 0;
    Vector3 target = new();
    public float idleTimeInSeconds;
    float idleTimeElapsed;
    public float minIdleTimeInSeconds;
    public float maxIdleTimeInSeconds;
    public Vector3 distanceToWalkPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
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
}
