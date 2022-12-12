using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionSystem : MonoBehaviour
{
    [Header("Object References")]
    public DetectionSystem detectionSystem;

    public float suspicionIncrement;
    public float suspicionMeter;

    [Header("Suspicion Thresholds")]
    public float searchSuspicionThreshold;
    public float chaseSuspicionThreshold;
    public float patrolSuspicionThreshold;

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

        //if they heard the coin, need to increase suspicion a lot
        //public method to increase suspicion a lot, called from detection system? or
        //additional boolean in detection system indicating they heard a coin, not a footstep

        if (detectionSystem.canSeePlayer)
        {
            suspicionMeter += suspicionIncrement * 20;
        }
        //call the decrease suspicion every few seconds

    }



    private IEnumerator DecreaseSuspicion()
    {
        //delay the execution so it only decreases every few seconds
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        //should always execute?
        while (true)
        {
            yield return wait;
            suspicionMeter -= 0.5f;
        }
        
    }
}
