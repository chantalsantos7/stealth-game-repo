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

    public float attackRadius;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        detectionSystem = GetComponent<DetectionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (detectionSystem.currentTarget == null)
        {
            detectionSystem.HandleDetection();
        }
    }

    
}
