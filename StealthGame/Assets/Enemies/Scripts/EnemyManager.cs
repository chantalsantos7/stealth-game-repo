using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Object References")] 
    public EnemyStateManager stateManager;
    public EnemyAnimatorManager animatorManager;
    public WeaponsInventory enemyInventory;
    public DetectionSystem detectionSystem;
    public GameObject swordModel;

    [HideInInspector] public bool isUnarmed;

    public float health;
    bool isPerformingAction;

    //public float attackRadius;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        detectionSystem = GetComponent<DetectionSystem>();
        animatorManager = GetComponent<EnemyAnimatorManager>();
        enemyInventory = GetComponent<WeaponsInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //HandleCurrentAction();
        isUnarmed = animatorManager.GetBool("IsUnarmed");
    }

    public void Disarm()
    {
        //reactivate the sword object on the model, deactivate the one in enemy's hand
        
    }
}
