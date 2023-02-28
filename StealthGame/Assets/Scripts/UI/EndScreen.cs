using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI guardsKilledText;
    [SerializeField] TextMeshProUGUI teleportUsedText;
    [SerializeField] TextMeshProUGUI distractUsedText;

    [SerializeField] GameObject prevStatsScreen;

    private void OnEnable()
    {
        var totalTime = GameManager.Instance.achievementTracker.TimeTaken;
        timerText.text = TimeSpan.FromSeconds(totalTime).ToString("HH:mm:ss");
        GameManager.Instance.achievementTracker.SaveGameSessionStats();
        guardsKilledText.text = "Guards killed: " + GameManager.Instance.achievementTracker.GuardsKilled;

        teleportUsedText.text = "Teleport ability used: " + GameManager.Instance.achievementTracker.TeleportUsed + " times";
        distractUsedText.text = "Distract ability used: " + GameManager.Instance.achievementTracker.DistractUsed + " times";
    }

    public void PreviousGameSessions()
    {
        prevStatsScreen.SetActive(true);
    }
}
