using SerializableClass;
using System;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    public bool TargetIsDead { get; set; }
    public static PlayerStatistics playerStats = new();
    public int GuardsKilled { get { return playerStats.guardsKilled; } set { playerStats.guardsKilled = value; } }
    public float TimeTaken { get { return playerStats.timeTaken; } private set { playerStats.timeTaken = value; } }
    public int TeleportUsed { get { return playerStats.teleportsUsed; } set { playerStats.teleportsUsed = value; } }
    public int DistractUsed { get { return playerStats.distractsUsed; } set { playerStats.distractsUsed = value; } }

    /*[SerializeField, HideInInspector] private int guardsKilled;
    [SerializeField, HideInInspector] private float timeTaken;
    [SerializeField, HideInInspector] private int teleportsUsed;
    [SerializeField, HideInInspector] private int distractsUsed;*/


    //how many times detected?
    private void Awake()
    {
        SaveGameSessionStats();
    }

    // Update is called once per frame
    void Update()
    {
        TimeTaken += Time.deltaTime;
    }

    public void SaveGameSessionStats()
    {
        string json = JsonUtility.ToJson(this);
        DateTime currentTime = DateTime.Now;
        string fileName = "stealthGame-" + currentTime.Day + "-" + currentTime.Month+ "-" + currentTime.Year + currentTime.Hour + currentTime.Minute + ".json";
        FileManager.WriteToFile(fileName, json);
    }

    

}
