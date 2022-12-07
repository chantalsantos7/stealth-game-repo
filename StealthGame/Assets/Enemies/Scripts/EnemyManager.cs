using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Object References")]
    public EnemyStateManager stateManager;
    public DetectionSystem detectionSystem;

    public float health;
    bool isPerformingAction;

    [Header("Detection Variables")]
    public float detectionRadius = 20;
    //min and max angles determines enemy FOV - lower min and raise max to expand it
    public float minDetectionAngle = -50;
    public float maxDetectionAngle = 50;

    public float attackRadius;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        detectionSystem = GetComponent<DetectionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (detectionSystem.currentTarget == null)
        {
            detectionSystem.HandleDetection();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //replace red with whatever color you prefer
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Vector3 fovLine1 = Quaternion.AngleAxis(maxDetectionAngle, transform.up) * transform.forward * detectionRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(minDetectionAngle, transform.up) * transform.forward * detectionRadius;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
