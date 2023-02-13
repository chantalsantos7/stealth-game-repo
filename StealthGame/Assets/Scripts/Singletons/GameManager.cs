using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager; 
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
        if (EnemyIsSuspicious)
        {
            uiManager.ToggleSuspicionIndicator(EnemyIsSuspicious);
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

    /*public void ReloadLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }*/
}
