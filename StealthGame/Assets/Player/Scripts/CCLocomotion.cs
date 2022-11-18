using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCLocomotion : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    private float playerSpeed = 2f;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(inputManager.horizontalInput, 0f, inputManager.verticalInput);
        characterController.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}
