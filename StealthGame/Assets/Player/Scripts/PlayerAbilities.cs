using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public float currentStamina;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    bool sprinting;
    private WaitForSeconds regenTick = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerManager = GetComponent<PlayerManager>();
        currentStamina = playerManager.maxStamina;
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
            //StopCoroutine(RechargeStamina());
            StopAllCoroutines();
            StartCoroutine(UseStamina(2));
            if (currentStamina <= 0)
            {
                playerLocomotion.IsSprinting = false;
            }
        }
        
        if (!playerLocomotion.IsSprinting)
        {
            StopCoroutine(UseStamina(2));
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
        //invoke this per second when stamina is 0
        yield return new WaitForSeconds(3f);
        while (currentStamina < playerManager.maxStamina)
        {
            currentStamina += 0.5f;
            yield return regenTick;
        }
    }   
}
