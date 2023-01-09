using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionSystem : MonoBehaviour
{
    [Tooltip("Amount of time in seconds before the distractor emits a sound.")]public float soundEmitDelay;
    public float enemyRange;
    public Collider[] enemies;
    public LayerMask enemyLayer;

    private void Awake()
    {
        StartCoroutine(EmitSound());
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy this object after a while?
    }

    private IEnumerator EmitSound() 
    {
        yield return new WaitForSeconds(soundEmitDelay);
        //BUG: Probable source of the memory leak errors that sometimes come up 
        enemies = Physics.OverlapSphere(transform.position, enemyRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                EnemyStateManager esm = enemies[i].GetComponent<EnemyStateManager>();
                if (esm != null) 
                {
                    //access the detection system through the ESM
                    //set the lastKnownPosition to the distractor's position
                    //then switch to search state
                    //set suspicionMeter to above the search threshold too so it doesn't immediately go back to patrol

                    esm.detectionSystem.lastKnownPosition = transform.position;
                    esm.detectionSystem.heardSomething = true;
                    esm.suspicionSystem.suspicionMeter = esm.searchSuspicionThreshold + 20;
                    //see if it switches to search state through this

                    
                }
            }
                
        }
        //play an audioClip here
        //get a reference to any enemies within hearing range
        //another physics raycast?
        //switch to the 
    }

    //Delete object after a while - also a coroutine? 
}
