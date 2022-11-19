using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxStamina = 500f;
    public float maxHealth = 100f;

    public float health;
    public float currentStamina;

    [Header("Cooldown Times")]
    public float teleportCooldown;

    [Header("Object References")]
    public Camera cameraObject;
    public CameraManager cameraManager;
    public LineRenderer lineRenderer;

    PlayerLocomotion playerLocomotion;
    InputManager inputManager;
    
    public bool teleportAiming { get; set; }
    bool canceledTeleport;
    bool sprinting;

    public Vector3 teleportPos { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        currentStamina = maxStamina;
        health = maxHealth;
        teleportAiming = false;
    }

    // Update is called once per frame
    void Update()
    { 
        
        
        if (playerLocomotion.IsSprinting)
        {
            StopCoroutine(RechargeStamina(1f));
            StartCoroutine(UseStamina(1f));
            if (currentStamina <= 0)
            {
                playerLocomotion.IsSprinting = false;
            }
        } else if (!playerLocomotion.IsSprinting)
        {
            StopCoroutine(UseStamina(1f));
            StartCoroutine(RechargeStamina(0.5f));
        }   

        if (teleportAiming)
        {
            teleportAiming = false;
            //rn being triggered by the press of T button
            //Teleport();
        }
    }

    private IEnumerator UseStamina(float amount)
    {
        //decrease the amount from current Stamina
        currentStamina -= amount;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator RechargeStamina(float amount)
    {
        //invoke this per second when stamina is 0
        while (currentStamina < maxStamina)
        {
            currentStamina += amount;
            yield return new WaitForSeconds(1f);
        }
    }

    public void AimTeleport()
    {
        Debug.Log("AimTeleport function reached");
        teleportAiming = !teleportAiming;

        if (teleportAiming)
        {
            cameraManager.cameraMode = CameraMode.AimTeleport;
            teleportPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        } else
        {
            cameraManager.cameraMode = CameraMode.Basic;
        }
        
        
        //get the worldPosition of where the user clicks, pass it on to the Teleport function
        
    }

    public void Teleport()
    {
        //var worldPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        playerLocomotion.playerRigidbody.position = teleportPos;
    }

   
}
