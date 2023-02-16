using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;

    private void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, 
            rotation * Vector3.up);


    }

    public void SetUp(string prompt)
    {
        promptText.text = prompt;
        uiPanel.SetActive(true);
        isDisplayed= true;
    }

    public void Close()
    {
        isDisplayed= false;
        uiPanel.SetActive(false);
    }
}
