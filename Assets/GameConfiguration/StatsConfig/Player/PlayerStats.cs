using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameConfiguration/PlayerStatsConfig", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float MovementSpeed;
    public int PlayerHP;
    public float PlayerArmor;
    public float PlayerDodge;
    public float PlayerCritChance;
    public float PlayerLuck;
    public float PlayerHPRegeneration;
    public float PlayerLifeSteal;
    public int MeleeDamage;
    public int RangeDamage;
    public float ElementalDamage;
    public float AttackSpeed;
    public float CritialDamagemultiple;
    public float AttackRange;

}