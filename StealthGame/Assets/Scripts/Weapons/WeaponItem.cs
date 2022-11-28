using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Attack Animations")]
    public List<string> leftAttackAnimations;
    public List<string> rightAttackAnimations;
    //public string[] heavyAttackAnims;
}
