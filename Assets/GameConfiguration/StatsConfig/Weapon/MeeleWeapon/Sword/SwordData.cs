using UnityEngine;


[CreateAssetMenu(fileName = "SwordData", menuName = "GameConfiguration/Weapon/MeleeWeapon/Sword", order = 1)]
public class SwordData : ScriptableObject
{
    public string SwordName;
    public float attackRange;
    public int attackDamage;
    public float critChance;
    public float attackDuration;
    public string Description;
    

}

