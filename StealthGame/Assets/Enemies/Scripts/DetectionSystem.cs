using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    //public float radius;
    //public float angle;

    [Header("Detection Variables")]
    public float sightDetectionRadius = 10;
    //min and max angles determines enemy FOV - lower min and raise max to expand it
    [Range(0, 360)]
    public float detectionAngle;
    
    public float minDetectionAngle = -50;
    public float maxDetectionAngle = 50;
    public float hearingDetectionRadius;

    public GameObject playerRef;

    EnemyManager enemyManager;
    public PlayerManager currentTarget;
    public LayerMask detectionLayer;
    public LayerMask obstructionLayer;

    public bool canSeePlayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        //permanently searching for the player
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightDetectionRadius, detectionLayer);
        if (rangeChecks.Length != 0 ) 
        {
            Transform target = rangeChecks[0].transform; //only the player on the detectionLayer, so only need to get the first entry in array
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward  , directionToTarget) < detectionAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    canSeePlayer = true;
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
        else
        {
            canSeePlayer = false;
        }
    }

    public void HandleDetection()
    {

        /*Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.sightDetectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            //try to get player manager script, storing it in variable player
            if (colliders[i].transform.TryGetComponent<PlayerManager>(out var player)) 
            {
                Vector3 targetDirection = player.transform.position - enemyManager.transform.forward; //not properly tracking the enemy's forward
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward); //should check the angle between the player and the enemy?
                Debug.Log("targetDirection: " + targetDirection + "; enemyManager.transform.forward: " + enemyManager.transform.forward);
                //the viewableAngle al
                if (viewableAngle > enemyManager.minDetectionAngle && viewableAngle < enemyManager.maxDetectionAngle)
                {
                    Debug.Log("viewableAngle: " + viewableAngle);
                    currentTarget = player;
                    //move to chase or search state as needed
                }
            }
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        /*Gizmos.color = Color.red; //replace red with whatever color you prefer
        Gizmos.DrawWireSphere(transform.position, sightDetectionRadius);
        Vector3 fovLine1 = Quaternion.AngleAxis(detectionAngle, transform.up) * transform.forward * sightDetectionRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(detectionAngle, transform.up) * transform.forward * sightDetectionRadius;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);*/
    }
}
