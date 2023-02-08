using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/WeaponDamage Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Attack Animations")]
    [Tooltip("The names of the attack animations that should be used by this weapon (these should be the same names as the states in the animator).")]
    public List<string> attackAnimations;
    //public string[] heavyAttackAnims;
}
