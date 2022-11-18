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
    public float maxSprintTime;

    public float health;
    public float currentStamina;
    

    [Header("Cooldown Times")]
    public float teleportCooldown;

    [Header("Object References")]
    public Camera cameraObject;
    public LineRenderer lineRenderer;

    PlayerLocomotion playerLocomotion;
    InputManager inputManager;
    
    bool teleportAiming;
    bool canceledTeleport;
    bool sprinting;

    // Start is called before the first frame update
    private void Awake()
    {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        currentStamina = maxStamina;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GetTeleportInput();
        TeleportAiming();
        var worldPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
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
        
    }

    private void Sprint()
    {
        //while player is sprinting, decrease stamina
        //once they're not
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

    public void TeleportAiming()
    {
        ///var worldPos = cameraObject.ScreenToWorldPoint(Mouse.current.position);
        if (teleportAiming)
        {
            Ray ray = cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("I'm looking at " + hit.transform.name);
                Vector3[] pointsArray = new[] { lineRenderer.transform.position, hit.point };
                lineRenderer.SetPositions(pointsArray);
                //Debug.DrawRay(transform.position, hit.point, Color.yellow);
            }
            else
            {
                Debug.Log("I'm looking at nothing!");
            }
            
            GetTeleportInput();

            if (!teleportAiming && !canceledTeleport)
            {
                HandleTeleport(hit);
            }
        }
        
        

    }

    private void GetTeleportInput()
    {
        teleportAiming = inputManager.teleportModifierPressed;
        canceledTeleport = inputManager.cancelTeleportKeyPressed;
    }

    public void HandleTeleport(RaycastHit hit)
    {
        //move the player to the position indicated by the raycast
        transform.position = hit.point;
    }

   
}
