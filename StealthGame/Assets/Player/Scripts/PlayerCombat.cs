using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    public void Attack(WeaponItem weapon)
    {
        //randomly select one of the available anims
        int weaponIndex = (int) Random.Range(0f, weapon.attackAnimations.Count);
        animatorManager.PlayTargetAnimation(weapon.attackAnimations[weaponIndex], true);
    }
}
