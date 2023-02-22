using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AchievementTracker : MonoBehaviour
{
    public bool TargetIsDead { get; set; }
    public int GuardsKilled { get { return guardsKilled; } set { guardsKilled = value; } }
    public float TimeTaken { get { return timeTaken; } private set { timeTaken = value; } }
    public int TeleportUsed { get { return teleportsUsed; } set { teleportsUsed = value; } }
    public int DistractUsed { get { return distractsUsed; } set { distractsUsed = value; } }

    [SerializeField, HideInInspector] private int guardsKilled;
    [SerializeField, HideInInspector] private float timeTaken;
    [SerializeField, HideInInspector] private int teleportsUsed;
    [SerializeField, HideInInspector] private int distractsUsed;


    //how many times detected?
    private void Awake()
    {
        //SaveGameSessionStats();
    }

    // Update is called once per frame
    void Update()
    {
        TimeTaken += Time.deltaTime;
    }

    public void SaveGameSessionStats()
    {
        string json = JsonUtility.ToJson(this);
        WriteToFile("lastSession.json", json);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using StreamWriter writer = new StreamWriter(fileStream);
        writer.Write(json);
        /*Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();*/

    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }


}
