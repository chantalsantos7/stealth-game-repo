using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public float currentStamina;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    bool sprinting;
    private WaitForSeconds regenTick = new WaitForSeconds(0.5f);

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();
        currentStamina = playerManager.maxStamina;
    }

    void Update()
    {
        Sprint();  
    }

    private void Sprint()
    {
        if (playerLocomotion.IsSprinting)
        {
            StopAllCoroutines();
            StartCoroutine(UseStamina(1));
            if (currentStamina <= 0)
            {
                playerLocomotion.IsSprinting = false;
            }
        }
        
        if (!playerLocomotion.IsSprinting)
        {
            StopAllCoroutines();
            if (currentStamina < playerManager.maxStamina)
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
        while (currentStamina < playerManager.maxStamina)
        {
            currentStamina += 1f;
            yield return regenTick;
        }
    }   
}
