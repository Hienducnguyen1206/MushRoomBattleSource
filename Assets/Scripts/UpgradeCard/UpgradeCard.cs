using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "UpgradeCardData", menuName = "GameConfiguration/UpgradeCard", order = 3)]
[System.Serializable]    
public class UpgradeCardData : ScriptableObject
{
    public string cardID;
    public string cardName;
    public int ReDrawCost;
    public int BuyCost;
    public CardTypeEnum cardType;
    public Sprite cardImage;       
    public List<CardAttribute> attributes = new List<CardAttribute>();
   

}

[Serializable]
public class CardAttribute
{
    public PlayerAtributeEnum attributeName;  
    public AtributeType atributeType;
    public float attributeValue;  

}

public enum CardTypeEnum { NORMAL, RARE, EPIC, LEGENDARY,LEVELUP }
public enum PlayerAtributeEnum
{
    PlayerMovementSpeed,
    PlayerMaxHp,
    PlayerArmor,
    PlayerDodge,
    PlayerCritialChance,
    PlayerLuck,
    PlayerHpRegentation,
    PlayerLifesteal,
    PlayerMeleeDamage,
    PlayerRangeDamage,
    PlayerElementalDamage,
    PlayerAttackSpeed,
    PlayerCritialDamageMultiple,
    PlayerAttackRange,
    
}
public enum AtributeType { INCREASE,DECREASE}

