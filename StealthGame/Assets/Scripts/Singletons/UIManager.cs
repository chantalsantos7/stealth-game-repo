using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject suspicionIndicator;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject abilityBar;
    [SerializeField] GameObject objectivesPanel;
    [SerializeField] GameObject teleportIcon;
    [SerializeField] GameObject distractIcon;
    [SerializeField] TMP_Text objectiveText;
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
        TurnOffUI();
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
        TurnOffUI();
        levelEndScreen.SetActive(true);
        PauseGame();
    }

    public void ToggleUI()
    {
        ToggleAbilityBar();
        ToggleObjectivesPanel();
    }

    public void TurnOffUI()
    {
        abilityBar.SetActive(false);
        objectivesPanel.SetActive(false);
    }

    public void ToggleAbilityBar()
    {
        abilityBar.SetActive(!abilityBar.activeSelf);
    }

    public void ToggleObjectivesPanel()
    {
        objectivesPanel.SetActive(!objectivesPanel.activeSelf);
    }

    public void ChangeObjective(string objective)
    {
        //can probably track using PLayer achievements i.e. if target is dead, change objective
        objectiveText.text = objective;
    }

    public void SetIconTransparency()
    {
        
    }
}
