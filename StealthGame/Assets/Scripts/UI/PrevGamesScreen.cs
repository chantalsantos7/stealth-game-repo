using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SerializableClass;
using TMPro;
using System;

public class PrevGamesScreen : MonoBehaviour
{
    private static List<string> prevGames = new();
    private static List<PlayerStatistics> playerStatistics = new();

    [SerializeField] private Transform scrollContentParent;

    [Header("Statistics Prefab Components")]
    [SerializeField] private GameObject statsPrefab;

    private void OnEnable()
    {
        GameManager.Instance.uiManager.EnableMouseCursor();
        //get all the json files in the persistent data path
        string[] jsonFiles = Directory.GetFiles(Application.persistentDataPath, "*.json");
        int counter = 0;
        foreach (string jsonFile in jsonFiles)
        {
            prevGames.Add(FileManager.ReadFromFile(jsonFile));
            playerStatistics.Add(JsonUtility.FromJson<PlayerStatistics>(prevGames[counter]));
            
            Debug.Log(prevGames[counter]);
            counter++;
        }

        foreach (var prevGame in playerStatistics)
        {
            var newObj = Instantiate(statsPrefab, scrollContentParent);
            var textBoxes = newObj.GetComponentsInChildren<TextMeshProUGUI>();
            textBoxes[2].text = TimeSpan.FromSeconds(prevGame.timeTaken).ToString();
            textBoxes[1].text = "Guards killed: " + prevGame.guardsKilled.ToString() + "\n"
                                + "Teleports used: " + prevGame.teleportsUsed.ToString() + "\n"
                                + "Distracts used: " + prevGame.distractsUsed.ToString();
        }

    }
}
