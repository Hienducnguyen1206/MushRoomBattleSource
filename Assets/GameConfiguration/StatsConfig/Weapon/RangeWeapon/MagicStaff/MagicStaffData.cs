using UnityEngine;


[CreateAssetMenu(fileName = "MagicStaffData", menuName = "GameConfiguration/Weapon/RangeWeapon/MagicStaff", order = 1)]
public class MagicStaffData : ScriptableObject
{
    public string staffName;
    public float attackRange;
    public int attackDamage;
    public float critChance;
    public float attackPerSecond;
    public float magicSpeed;
    public string Description;

}

