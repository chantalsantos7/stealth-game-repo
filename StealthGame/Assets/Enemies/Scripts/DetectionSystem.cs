using System;
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
    public bool inAttackRange = false;
    public Vector3 lastKnownPosition;

    private Collider[] fovRangeCheck = new Collider[1];
    private Collider[] hearingRangeCheck = new Collider[1];
    private Collider[] attackRangeCheck = new Collider[1];

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
        //Collider[] rangeCheck = new Collider[2];
        Physics.OverlapSphereNonAlloc(transform.position, sightDetectionRadius, fovRangeCheck, detectionLayer);
        if (fovRangeCheck[0] == null)
        {
            canSeePlayer = false;
            return;
        }

        for (int i = 0; i < fovRangeCheck.Length; i++)
        {
            if (fovRangeCheck[i] != null && fovRangeCheck[i].gameObject.CompareTag("Player"))
            {
                Transform target = fovRangeCheck[i].transform;
                 //check whether the target is w/in their viewing angle
                Vector3 directionToTarget = (target.position - transform.position).normalized; 
                if (Vector3.Angle(transform.forward, directionToTarget) < detectionAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    //check if anything is obstructing the player
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
            else
            {
                canSeePlayer = false;
            }
        }

    }

    private void HearingDetection()
    {
        Physics.OverlapSphereNonAlloc(transform.position, hearingDetectionRadius, hearingRangeCheck, detectionLayer);
        /*Collider[] rangeCheck = new Collider[2];
        Physics.OverlapSphereNonAlloc(transform.position, hearingDetectionRadius, rangeCheck, detectionLayer);*/
        if (hearingRangeCheck[0] == null)
        {
            heardSomething = false;
            return;
        }

        for (int i = 0; i < hearingRangeCheck.Length; i++)
        {
            if (hearingRangeCheck[i] != null && hearingRangeCheck[i].transform.TryGetComponent<PlayerLocomotion>(out var player))
            {
                if (!player.IsCrouched &&
                        player.IsMoving) //can only hear the player if they are not crouched
                {
                    //enter searching state, start going to position of that sound
                    lastKnownPosition = hearingRangeCheck[i].transform.position;
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
        //Debug.Log("Check something");
        Physics.OverlapSphereNonAlloc(transform.position, attackRadius, attackRangeCheck, detectionLayer);
        
        for (int i = 0; i < attackRangeCheck.Length; i++)
        {
            if (attackRangeCheck[i] != null && attackRangeCheck[i].gameObject.CompareTag("Player"))
            {
                inAttackRange = true;
            }
            else
            {
                //Array.Clear(attackRangeCheck, 0, attackRangeCheck.Length);
                attackRangeCheck[i] = null;
                inAttackRange = false;
            }
        }
        

        if (attackRangeCheck[0] == null)
        {
            inAttackRange = false;
            //return;
        }
    }
}
