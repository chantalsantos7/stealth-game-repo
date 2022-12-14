using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionSystem : MonoBehaviour
{
    [Header("Object References")]
    public DetectionSystem detectionSystem;

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

    private void Awake()
    {
        detectionSystem = GetComponent<DetectionSystem>();
    }

    private void Start()
    {
        StartCoroutine(DecreaseSuspicion());
    }
    // Start is called before the first frame update
     
    // Update is called once per frame
    void Update()
    {
        if (detectionSystem.heardSomething)
        {
            suspicionMeter += suspicionIncrement;
        }
         
        if (detectionSystem.canSeePlayer)
        {
            suspicionMeter += suspicionIncrement * playerSeenIncreaseMultiplier;
        }
        //call the decrease suspicion every few seconds

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
