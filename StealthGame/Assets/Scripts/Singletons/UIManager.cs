using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject suspicionIndicator;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject abilityBar;

    private void Start()
    {
        DisableMouseCursor();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ToggleDeathScreen()
    {
        //activate mouse cursor again
        EnableMouseCursor();
        PauseGame();
        deathScreen.SetActive(!deathScreen.activeSelf);
    }

    public void DisableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EnableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToggleSuspicionIndicator(bool value)
    {
        suspicionIndicator.SetActive(value);
    }

    public void EnableEndScreen()
    {
        levelEndScreen.SetActive(true);
        PauseGame();
    }

    public void ToggleAbilityBar()
    {
        abilityBar.SetActive(!abilityBar.activeSelf);
    }
}
