using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionSystem : MonoBehaviour
{
    [Header("Object References")]
    public DetectionSystem detectionSystem;
    public EnemyManager enemyManager;

    [Header("Meter Values")]
    public float suspicionIncrement;
    public float suspicionDecrement;
    public float suspicionMeter;

    [Header("Suspicion Thresholds & Multipliers")]
    public float searchSuspicionThreshold;
    public float chaseSuspicionThreshold;
    public float patrolSuspicionThreshold;
    public float heardDistractionMultiplier;
    public float playerSeenIncreaseMultiplier;
    public float inAttackRangeMultiplier;

    private void Awake()
    {
        detectionSystem = GetComponent<DetectionSystem>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        StartCoroutine(DecreaseSuspicion());
    }
  
    void Update()
    {
        if (!enemyManager.isDead)
        {
            if (detectionSystem.heardSomething)
            {
                suspicionMeter += suspicionIncrement;
            }

            if (detectionSystem.canSeePlayer)
            {
                suspicionMeter += suspicionIncrement * playerSeenIncreaseMultiplier;
            }

            if (detectionSystem.foundBody)
            {
                suspicionMeter += searchSuspicionThreshold + 200;
            }

            if (detectionSystem.inAttackRange)
            {
                suspicionMeter += suspicionIncrement * inAttackRangeMultiplier;
            }
        }
    }

    private IEnumerator DecreaseSuspicion()
    {
        //delay the execution so it only decreases every few seconds
        float delay = 0.5f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        //should always execute?
        while (true)
        {
            yield return wait;
            if (suspicionMeter > 0)
            {
                suspicionMeter -= suspicionDecrement;
            }
        }
        
    }
}
