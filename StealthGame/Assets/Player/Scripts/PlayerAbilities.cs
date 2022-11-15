using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Player Stats")]
    public float health = 100f;
    public float stamina = 75f;

    [Header("Cooldown Times")]
    public float teleportCooldown;

    [Header("Object References")]
    public Camera cameraObject;
    public LineRenderer lineRenderer;

    PlayerLocomotion playerLocomotion;
    InputManager inputManager;
    
    bool teleportAiming;
    bool canceledTeleport;

    // Start is called before the first frame update
    private void Awake()
    {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetTeleportInput();
        TeleportAiming();
        var worldPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Debug.Log(worldPos);
        
    }

    private void Sprint()
    {

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
