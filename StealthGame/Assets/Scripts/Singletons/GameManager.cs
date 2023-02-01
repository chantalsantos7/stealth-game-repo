using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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


    public UIManager uiManager;

    public bool PlayerIsDead { get; set; }

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

    public void HandlePlayerDeath()
    {
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
