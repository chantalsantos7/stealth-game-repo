using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBook : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        GameManager.Instance.EndLevelSequence();
        return true;
    }
}
