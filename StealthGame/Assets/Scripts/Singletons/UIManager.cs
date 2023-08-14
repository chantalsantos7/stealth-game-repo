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

    public GameObject target;
    public GameObject exitRelic;

    private void Start()
    {
        DisableMouseCursor();
        ToggleOutline(target, true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
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

    public void ToggleOutline(GameObject outlinedObject, bool value)
    {
        outlinedObject.GetComponent<Outline>().enabled = value;
    }


    #region End & Death Screens
    
    public void TurnOffUI()
    {
        abilityBar.SetActive(false);
        objectivesPanel.SetActive(false);
        suspicionIndicator.SetActive(false);
    }
    
    public void EnableEndScreen()
    {
        TurnOffUI();
        EnableMouseCursor();
        levelEndScreen.SetActive(true);
        PauseGame();
    }
    
    public void ToggleDeathScreen()
    {
        EnableMouseCursor();
        PauseGame();
        TurnOffUI();
        deathScreen.SetActive(!deathScreen.activeSelf);
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

    #endregion

    #region Objectives

    public void ToggleObjectivesPanel()
    {
        objectivesPanel.SetActive(!objectivesPanel.activeSelf);
        if (target.GetComponent<Outline>().enabled)
            target.GetComponent<Outline>().enabled = false;
        else
            target.GetComponent<Outline>().enabled = true;

        if (exitRelic.GetComponent<Outline>().enabled)
            exitRelic.GetComponent<Outline>().enabled = false;
        else
            exitRelic.GetComponent<Outline>().enabled = true;
    }

    public void ChangeObjective(string objective)
    {
        objectiveText.text = objective;
    }
    #endregion

    #region Ability Bar

    public void ToggleDistractionDeployText()
    {
        distractDeployTxt.gameObject.SetActive(!distractDeployTxt.gameObject.activeSelf);
    }

    public void ToggleTeleportDeployText()
    {
        teleportDeployTxt.gameObject.SetActive(!teleportDeployTxt.gameObject.activeSelf);
    }

    public void SetTeleportIconTransparency(float value)
    {
        var images = teleportIcon.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.SetTransparency(value);
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
