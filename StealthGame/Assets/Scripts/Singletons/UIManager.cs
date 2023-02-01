using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        DisableMouseCursor();
    }

    public void ToggleDeathScreen()
    {
        //activate mouse cursor again
        EnableMouseCursor();
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
}
