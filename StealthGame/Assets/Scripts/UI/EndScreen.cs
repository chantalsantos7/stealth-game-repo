using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI guardsKilledText;

    private void OnEnable()
    {
        var totalTime = GameManager.Instance.achievementTracker.TimeTaken;
        timerText.text = TimeSpan.FromSeconds(totalTime).ToString();
        guardsKilledText.text = "Guards killed: " + GameManager.Instance.achievementTracker.GuardsKilled;
    }
}
