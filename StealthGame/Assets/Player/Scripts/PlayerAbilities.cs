using System.Collections;
using UnityEngine;

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
    public Transform cameraObject;
    public CameraManager cameraManager;
    public LineRenderer lineRenderer;
    public GameObject teleportView;
    Rigidbody teleportRigidbody;
    //public GameObject TeleportViewCam;

    PlayerLocomotion playerLocomotion;
    InputManager inputManager;

    [Header("Teleport View Movement")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public bool teleportAiming { get; set; }
    bool canceledTeleport;
    bool sprinting;

    public Vector3 TeleportPos { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        cameraObject = Camera.main.transform;
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        teleportRigidbody = teleportView.GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        health = maxHealth;
        teleportAiming = false;
    }

    // Update is called once per frame
    void Update()
    {

        Sprint();
        TeleportViewMove();
   
    }

    private void Sprint()
    {
        if (playerLocomotion.IsSprinting)
        {
            StopCoroutine(RechargeStamina(1f));
            StartCoroutine(UseStamina(1f));
            if (currentStamina <= 0)
            {
                playerLocomotion.IsSprinting = false;
            }
        }
        else if (!playerLocomotion.IsSprinting)
        {
            StopCoroutine(UseStamina(1f));
            if (currentStamina < maxStamina)
            {
                StartCoroutine(RechargeStamina(0.5f));
            }
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
        teleportAiming = !teleportAiming;

        if (teleportAiming)
        {
            Debug.Log("Teleport mode entered");
            Debug.Log(teleportAiming);
            cameraManager.cameraMode = CameraMode.AimTeleport;
            teleportView.transform.position = transform.position + Vector3.right;
            //freeze player movement
            playerLocomotion.canMove = false;
            //deactivate the model, switch to TVB (and TVB cam)
             teleportView.SetActive(true);
            //disable the third person controller
            //move the FP controller to player position
            //enable the TVB
            
            //teleportPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        } else
        {
            cameraManager.cameraMode = CameraMode.Basic;
            playerLocomotion.canMove = true;
            //playerLocomotion.enabled = true;
            //inputManager.enabled = true;
            Debug.Log(teleportAiming);
            Debug.Log("Teleport mode left");
        }
        //get the worldPosition of where the user clicks, pass it on to the Teleport function
        
    }

    private void TeleportViewMove()
    {
        if (!teleportAiming) return;
        Vector3 movementVelocity = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        movementVelocity += cameraObject.right * inputManager.horizontalInput;
        movementVelocity.Normalize();
        movementVelocity.y = 0f;

        teleportRigidbody.velocity = movementVelocity * movementSpeed;

        if (movementVelocity == Vector3.zero)
        {
            movementVelocity = teleportRigidbody.transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        Quaternion playerRotation = Quaternion.Slerp(teleportRigidbody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        teleportRigidbody.transform.rotation = playerRotation;
    }

    public void Teleport()
    {
        //var worldPos = cameraObject.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        TeleportPos = teleportView.transform.position;
        teleportView.SetActive(false);
        playerLocomotion.playerRigidbody.position = TeleportPos;
        cameraManager.cameraMode = CameraMode.Basic;
        playerLocomotion.canMove = true;
    }

   
}
