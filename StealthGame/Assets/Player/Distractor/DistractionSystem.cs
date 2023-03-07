using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionSystem : MonoBehaviour
{
    [Tooltip("Amount of time in seconds before the distractor emits a sound.")]public float soundEmitDelay;
    public float enemyRange;
    public Collider[] enemies;
    public LayerMask enemyLayer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundEffect;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(EmitSound());
        StartCoroutine(DestroyItem());
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
                if (enemies[i].TryGetComponent<EnemyStateManager>(out var esm)) 
                {
                    //access the detection system through the ESM
                    //set the lastKnownPosition to the distractor's position
                    //then switch to search state
                    //set suspicionMeter to above the search threshold too so it doesn't immediately go back to patrol

                    esm.detectionSystem.lastKnownPosition = transform.position;
                    esm.detectionSystem.heardSomething = true;
                    esm.suspicionSystem.suspicionMeter = esm.searchSuspicionThreshold + 75;
                }
            }
        }
        audioSource.Play();
    }

    private IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(120f);
        Destroy(gameObject);
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyRange);
    }

    //Delete object after a while - also a coroutine? 
}
