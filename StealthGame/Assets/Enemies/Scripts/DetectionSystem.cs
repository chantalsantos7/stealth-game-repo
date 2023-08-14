using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionSystem : MonoBehaviour
{
    [HideInInspector] public EnemyManager enemyManager;

    [Header("Object References")]
    public GameObject playerRef;
    public GameObject crouchCheckObj;

    [Header("Detection Variables")]
    public float sightDetectionRadius = 10;
    public float hearingDetectionRadius;
    public float attackRadius = 1f;
    [Range(0, 360)] public float detectionAngle;
    public LayerMask detectionLayer;
    public LayerMask obstructionLayer;

    public float timeSinceLastDetected;
    [HideInInspector] public PlayerManager currentTarget;

    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public bool heardSomething;
    [HideInInspector] public bool heardDistraction;
    [HideInInspector] public bool lookingAtObstruction;
    [HideInInspector] public bool inAttackRange = false;
    [HideInInspector] public Vector3 lastKnownPosition;

    private Collider[] fovRangeCheck = new Collider[1];
    private Collider[] hearingRangeCheck = new Collider[1];

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        playerRef = GameManager.Instance.player;
        StartCoroutine(DetectionRoutine());
    }

    private void Update()
    {
        timeSinceLastDetected += Time.deltaTime;
    }

    /*Calls the functions that check whether the player is within this enemy's search radius
    The checks need to be executed regularly so the enemy can react appropriately to player movements but to avoid them searching
    for the player in every frame, this coroutine only executes 5 times per second*/
    private IEnumerator DetectionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        while (true && !enemyManager.isDead)
        {
            yield return wait;
            FieldOfViewCheck();
            HearingDetection();
            AttackRangeCheck();
        }
    }
    
    /*The following function all follow the same detection principle, as they check whether the player's collider is within a certain radius to the enemy's position
      Each routine has different conditions to determine whether the player fulfills them */

    /*Checks whether the player is within the enemy's field of view */
    private void FieldOfViewCheck()
    {
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
                        Debug.Log("Time since last detected: " + timeSinceLastDetected);
                        canSeePlayer = true;
                        timeSinceLastDetected = 0;
                    }
                    else
                    {
                        //Lower starting position for a raycast, to see if player can be seen but is crouching
                        if (!Physics.Raycast(crouchCheckObj.transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                        {
                            Debug.Log("Time since last detected: " + timeSinceLastDetected);
                            canSeePlayer = true;
                            timeSinceLastDetected = 0;
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

    /*Checks whether the enemy has heard a noise*/
    private void HearingDetection()
    {
        Physics.OverlapSphereNonAlloc(transform.position, hearingDetectionRadius, hearingRangeCheck, detectionLayer);
       
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
                        player.IsMoving) //can only hear the player if they are not in stealth mode
                {
                    //enter searching state, start going to position of that sound
                    lastKnownPosition = hearingRangeCheck[i].transform.position;
                    heardSomething = true;
                }
                else
                    heardSomething = false;
                
            }
            else
                heardSomething = false;
            
        }
    }

    private void AttackRangeCheck()
    {
        Collider[] attackRangeCheck = Physics.OverlapSphere(transform.position, attackRadius, detectionLayer);
        
        for (int i = 0; i < attackRangeCheck.Length; i++)
        {
            if (attackRangeCheck[i] != null && attackRangeCheck[i].gameObject.CompareTag("Player"))
                inAttackRange = true;
            else
                inAttackRange = false;
        }
        

        if (attackRangeCheck.Length == 0)
            inAttackRange = false;
    }
}
