using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrevGamesScreen : MonoBehaviour
{
    private static List<string> prevGames = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //get all the json files in the persistent data path
        string[] jsonFiles = Directory.GetFiles(Application.persistentDataPath, "*.json");
        int counter = 0;
        foreach (string jsonFile in jsonFiles)
        {
            prevGames.Add(FileManager.ReadFromFile(jsonFile));
            Debug.Log(prevGames[counter]);
            counter++;
        }
    }
}
