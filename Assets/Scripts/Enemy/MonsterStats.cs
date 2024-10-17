using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [SerializeField]
    MonsterData monsterData;
    public int startHP;
    public float currentMoveSpeed;   
    public int currentAttackDamage;
    public float currentAttackRange;
    public float currentAttackPerSecond;
    public int currentArmor;
    public int currentMagicResistance;

    

    private void OnEnable()
    {
        startHP = monsterData.HP + GameController.Instance.currentWave*50;
        currentMoveSpeed = monsterData.MoveSpeed;
        currentAttackDamage = monsterData.AttackDamage + GameController.Instance.currentWave*5;
        currentAttackRange = monsterData.AttackRange;
        currentAttackPerSecond = monsterData.AttackPerSecond;
        currentArmor = monsterData.Armor;
        currentMagicResistance = monsterData.MagicResistance;
    }

}
