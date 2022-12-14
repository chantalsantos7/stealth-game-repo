using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Attack Animations")]
    [Tooltip("The names of the attack animations that should be used by this weapon (these should be the same names as in the enemy animator).")]
    public List<string> attackAnimations;
    //public string[] heavyAttackAnims;
}
