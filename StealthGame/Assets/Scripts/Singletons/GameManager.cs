using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public AchievementTracker achievementTracker;
    public DamageVignette vignette;
    public FileManager fileManager;
    //use this to keep track of game stats
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
   
    public bool PlayerIsDead { get; set; }
    public bool EnemyIsSuspicious { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
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
            uiManager.ChangeObjective("Escape the level from one of the back towers.");
        }
    }

    public void HandlePlayerDeath()
    {
        //invoke ToggleDeathScreen after a few seconds, to show vignette and character crumpling
        //turn on DeathScreen
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
