using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionSystem : MonoBehaviour
{
    [Header("Object References")]
    public DetectionSystem detectionSystem;
    public EnemyManager enemyManager;

    [Header("Meter Values")]
    [Tooltip("Value by which the suspicion meter will increase.")]
    public float suspicionIncrement;
    [Tooltip("Value by which the suspicion meter will decrease.")]
    public float suspicionDecrement;
    [HideInInspector]public float suspicionMeter;

    [Header("Suspicion Thresholds & Multipliers")]
    public float searchSuspicionThreshold;
    public float chaseSuspicionThreshold;
    public float patrolSuspicionThreshold;
    public float heardDistractionMultiplier;
    public float playerSeenIncreaseMultiplier; //ensures suspicion meter will rapidly increase once enemy catches sight of player
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

            if (detectionSystem.inAttackRange)
            {
                suspicionMeter += suspicionIncrement * inAttackRangeMultiplier;
            }
        }
    }

    /* Decreases the enemy's suspicion once it has been raised, fires twice per second */
    private IEnumerator DecreaseSuspicion()
    {
        float delay = 0.5f;
        WaitForSeconds wait = new WaitForSeconds(delay);

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
