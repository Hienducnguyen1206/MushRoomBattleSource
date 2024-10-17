using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterData", menuName = "GameConfiguration/MonsterDataConfig", order = 3)]
[System.Serializable]
public class MonsterData  : ScriptableObject
{
   
    public string Name;
    public float MoveSpeed;
    public int HP;
    public int AttackDamage;
    public float AttackRange;
    public float AttackPerSecond;
    public int Armor;
    public int MagicResistance;
    public int MinCoin;
    public int MaxCoin;
    public int Exp;
    public float percentToSpawnNormalChest;
    public float percentToSpawnRareChest;
    public float percentToSpawnEpicChest;
    public float percentToSpawnLegendaryChest;
}

