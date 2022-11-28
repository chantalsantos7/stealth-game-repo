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
    public float staminaCooldown;

    [Header("Object References")]
    public Transform cameraObject;
    public CameraManager cameraManager;

    PlayerLocomotion playerLocomotion;
    InputManager inputManager;

    bool sprinting;

    public Vector3 TeleportPos { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        cameraObject = Camera.main.transform;
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        currentStamina = maxStamina;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();  
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
}
