using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    public bool TargetIsDead { get; set; }
    public int GuardsKilled { get; set; }
    public float TimeTaken { get; private set; }

    //how many times detected?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeTaken += Time.deltaTime;
    }
}
