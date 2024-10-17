using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunData", menuName = "GameConfiguration/Weapon/RangeWeapon/Gun", order = 0)]
public class GunData: ScriptableObject
{
    public string gunName;
    public float attackRange;
    public int attackDamage;
    public float critChance;
    public float attackPerSecond;
    public float bulletSpeed;
    public float recoilDistance;
    public float recoilDuration;
    public string Description;
}

