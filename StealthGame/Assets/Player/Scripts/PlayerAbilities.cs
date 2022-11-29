using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxStamina = 75;
    public float maxHealth = 100f;

    public float health;
    public int currentStamina;

    [Header("Cooldown Times")]
    public float teleportCooldown;
    public float staminaCooldown;

    PlayerLocomotion playerLocomotion;

    bool sprinting;
    private WaitForSeconds regenTick = new WaitForSeconds(0.5f);

    public Vector3 TeleportPos { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        
        playerLocomotion = GetComponent<PlayerLocomotion>();
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
            StopCoroutine(RechargeStamina());
            StartCoroutine(UseStamina(1));
            if (currentStamina <= 0)
            {
                playerLocomotion.IsSprinting = false;
            }
        }
        else if (!playerLocomotion.IsSprinting)
        {
            StopCoroutine(UseStamina(1));
            if (currentStamina < maxStamina)
            {
                StartCoroutine(RechargeStamina());
            }
        }
    }

    private IEnumerator UseStamina(int amount)
    {
        //decrease the amount from current Stamina
        currentStamina -= amount;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator RechargeStamina()
    {
        //invoke this per second when stamina is 0
        yield return new WaitForSeconds(3f);
        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            yield return regenTick;
        }
    }   
}
