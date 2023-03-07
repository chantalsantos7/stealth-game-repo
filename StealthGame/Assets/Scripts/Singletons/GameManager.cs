using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return instance;
        }
    }

    public UIManager uiManager;
    public AchievementTracker achievementTracker;
    public DamageVignette vignette;
    public FileManager fileManager;

    public GameObject player;
   
   
    public bool PlayerIsDead { get; set; }
    public bool EnemyIsSuspicious { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        uiManager.ToggleSuspicionIndicator(EnemyIsSuspicious);
        if (achievementTracker.TargetIsDead)
        {
            uiManager.ChangeObjective("Find the teleportation spell book to escape the castle.");
            uiManager.ToggleOutline(uiManager.target, false);
            uiManager.ToggleOutline(uiManager.exitRelic, true);
        }
    }

    public void HandlePlayerDeath()
    {
        if (uiManager != null)
        {
            uiManager.ToggleDeathScreen();
        }
    }

    public void EndLevelSequence()
    {
        achievementTracker.SaveGameSessionStats();
        uiManager.EnableEndScreen();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("GameLevel");
        Time.timeScale = 1.0f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
