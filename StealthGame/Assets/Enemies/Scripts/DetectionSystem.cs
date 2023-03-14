using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    public EnemyManager enemyManager;

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

    [HideInInspector] public PlayerManager currentTarget;

    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public bool heardSomething;
    [HideInInspector] public bool heardDistraction;
    [HideInInspector] public bool lookingAtObstruction;
    [HideInInspector] public bool foundBody;
    [HideInInspector] public bool inAttackRange = false;
    [HideInInspector] public Vector3 lastKnownPosition;

    private Collider[] fovRangeCheck = new Collider[3];
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

    //This coroutine only executes 5 times per second, so enemy is not searching for the player in every frame, lessening the performance load
    private IEnumerator DetectionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        //permanently searching for the player, unless they're dead
        while (true && !enemyManager.isDead)
        {
            yield return wait;
            FieldOfViewCheck();
            HearingDetection();
            AttackRangeCheck();
        }
    }

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
            if (fovRangeCheck[i] != null &&
                (fovRangeCheck[i].gameObject.CompareTag("Player") || fovRangeCheck[i].gameObject.CompareTag("Enemy")))
            {
                Transform target = fovRangeCheck[i].transform;
                 //check whether the target is w/in their viewing angle
                Vector3 directionToTarget = (target.position - transform.position).normalized; 
                if (Vector3.Angle(transform.forward, directionToTarget) < detectionAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (fovRangeCheck[i].gameObject.TryGetComponent(out EnemyManager fellowGuard))
                    {
                        if (fellowGuard.isDead)
                        {
                            foundBody = true;
                            Debug.Log(fellowGuard.gameObject.name + " foundBody: " + foundBody);
                        }
                    }
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer) && fovRangeCheck[i].gameObject.CompareTag("Player"))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        //Lower starting position for a raycast, to see if player can be seen but is crouching
                        if (!Physics.Raycast(crouchCheckObj.transform.position, directionToTarget, distanceToTarget, obstructionLayer) && fovRangeCheck[i].gameObject.CompareTag("Player"))
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
                foundBody = false;
                //checking if enemy is looking at wall, so they can be rotated
                if (Physics.Raycast(transform.position, Vector3.forward, sightDetectionRadius, obstructionLayer))
                {
                    lookingAtObstruction = true;
                }
                else
                {
                    lookingAtObstruction = false;
                }
            }
        }

    }

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
                        player.IsMoving) //can only hear the player if they are not crouched
                {
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
