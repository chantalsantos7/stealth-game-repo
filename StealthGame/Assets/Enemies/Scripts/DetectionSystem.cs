using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    //public float radius;
    //public float angle;
    [Header("Object References")]
    public GameObject playerRef;
    public GameObject crouchCheckObj;
    EnemyManager enemyManager;

    [Header("Detection Variables")]
    public float sightDetectionRadius = 10;
    public float hearingDetectionRadius;
    public float attackRadius = 1f;
    [Range(0, 360)] public float detectionAngle;
    public LayerMask detectionLayer;
    public LayerMask obstructionLayer;

    public PlayerManager currentTarget;
    

    public bool canSeePlayer;
    public bool heardSomething;
    public bool heardDistraction;
    public bool inAttackRange;
    public Vector3 lastKnownPosition;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DetectionRoutine());
    }

    //This coroutine only executes 5 times per second, so enemy is not constantly searching for the player, lessening the performance load
    private IEnumerator DetectionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        //permanently searching for the player
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            HearingDetection();
            AttackRangeCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        //BUG: Probable source of the memory leak errors that sometimes come up 
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightDetectionRadius, detectionLayer);
        
        if (rangeChecks.Length == 0)
        {
            canSeePlayer = false;
            return;
        }

        for (int i = 0; i < rangeChecks.Length; i++)
        {
            if (rangeChecks[i].gameObject.CompareTag("Player"))
            {
                Transform target = rangeChecks[i].transform;
                 //only the player will be on the detectionLayer, so only need to get the first entry in array
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < detectionAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        //Lower starting position for a raycast, to see if player can be seen but is crouching
                        if (!Physics.Raycast(crouchCheckObj.transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                        {
                            canSeePlayer = true;
                        } 
                        else
                        {
                            canSeePlayer = false;
                        }
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }

    }

    private void HearingDetection()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, hearingDetectionRadius, detectionLayer);
        
        if (rangeCheck.Length == 0)
        {
            heardSomething = false;
            return;
        }

        for (int i = 0; i < rangeCheck.Length; i++)
        {            
            if (rangeCheck[i].transform.TryGetComponent<PlayerLocomotion>(out var player))
            {
                if (!player.IsCrouched &&
                    player.IsMoving) //can only hear the player if they are not crouched
                {
                    //enter searching state, start going to position of that sound
                    lastKnownPosition = rangeCheck[i].transform.position;
                    heardSomething = true;
                }
                else
                {
                    heardSomething = false;
                }
            }
            else
            {
                heardSomething = false;
                
            }
        }

        
    }

    private void AttackRangeCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, attackRadius, detectionLayer);
        
        if (rangeCheck.Length == 0) {
            inAttackRange = false;
            return;
        }

        if (rangeCheck[0].gameObject.CompareTag("Player"))
        {
            inAttackRange = true;
        }
        /*else
        {
            inAttackRange = false;
        }*/

    }
}
