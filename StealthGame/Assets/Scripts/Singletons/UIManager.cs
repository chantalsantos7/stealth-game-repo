using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject prevStatsScreen;

    [Header("In-Game UI")]
    [SerializeField] GameObject abilityBar;
    [SerializeField] GameObject suspicionIndicator;
    [SerializeField] GameObject objectivesPanel;
    [SerializeField] GameObject teleportIcon;
    [SerializeField] GameObject distractIcon;
    [SerializeField] TMP_Text objectiveText;
    [SerializeField] TextMeshProUGUI teleportDeployTxt;
    [SerializeField] TextMeshProUGUI distractDeployTxt;

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
        EnableMouseCursor();
        levelEndScreen.SetActive(true);
        PauseGame();
    }

    public void EnablePrevStatsScreen()
    {
        prevStatsScreen.SetActive(true);
        levelEndScreen.SetActive(false);
    }

    public void BackButton()
    {
        prevStatsScreen.SetActive(false);
        EnableEndScreen();
    }

    public void TurnOffUI()
    {
        abilityBar.SetActive(false);
        objectivesPanel.SetActive(false);
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

    #region Ability Bar

    public void ToggleDistractDeploy()
    {
        distractDeployTxt.gameObject.SetActive(!distractDeployTxt.gameObject.activeSelf);
    }

    public void ToggleTeleportDeploy()
    {
        teleportDeployTxt.gameObject.SetActive(!teleportDeployTxt.gameObject.activeSelf);
    }

    public void SetTeleportIconTransparency(float value)
    {
        var images = teleportIcon.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.SetTransparency(value);
            //Debug.Log(img.color.a.ToString());
        }
    }

    public void SetDistractIconTransparency(float value)
    {
        var images = distractIcon.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.SetTransparency(value);
        }
    }

    public void ToggleAbilityBar()
    {
        abilityBar.SetActive(!abilityBar.activeSelf);
    }

    #endregion




}
