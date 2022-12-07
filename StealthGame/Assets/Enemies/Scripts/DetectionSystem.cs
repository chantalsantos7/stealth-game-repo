using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    EnemyManager enemyManager;
    public PlayerManager currentTarget;
    public LayerMask detectionLayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            //get player manager script
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();
            if (player != null) 
            {
                Vector3 targetDirection = player.transform.position - enemyManager.transform.forward; //not properly tracking the enemy's forward
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
                //the viewableAngle al
                if (viewableAngle > enemyManager.minDetectionAngle && viewableAngle < enemyManager.maxDetectionAngle)
                {
                    Debug.Log(viewableAngle);
                    currentTarget = player;
                }
            }
        }
    }
}
