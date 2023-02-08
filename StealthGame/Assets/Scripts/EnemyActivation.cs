 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    public GameObject prevAreaEnemies;
    public GameObject nextAreaEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //prevAreaEnemies.SetActive(true);
        //nextAreaEnemies.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //activate the enemies in the next area, deactivate the ones in the previous area (though will also need to account for user going back the other way)
            //set which one should be activated at start, reverse when this is triggered
            prevAreaEnemies.SetActive(!prevAreaEnemies.activeSelf);
            if (nextAreaEnemies != null)
            {
                nextAreaEnemies.SetActive(!nextAreaEnemies.activeSelf);
            }
            
        }
    }
}
