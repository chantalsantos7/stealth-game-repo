 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    public GameObject[] prevAreaEnemies;
    public GameObject[] nextAreaEnemies;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var enemyGroup in prevAreaEnemies)
            {
                if (enemyGroup != null)
                {
                    enemyGroup.SetActive(!enemyGroup.activeSelf);
                }
            }

            foreach (var enemyGroup in nextAreaEnemies)
            {
                if (enemyGroup != null)
                {
                    enemyGroup.SetActive(!enemyGroup.activeSelf);
                }       
            }
        }
    }
}
