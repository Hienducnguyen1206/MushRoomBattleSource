using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentStats : Singleton<PlayerCurrentStats>
{ [SerializeField]
    PlayerStats playerStats;
    public float currentMovementSpeed;
    public int currentPlayerHP;
    public float currentPlayerArmor;
    public float currentPlayerDodge;
    public float currentPlayerCritChance;
    public float currentPlayerLuck;
    public float currentPlayerHPRegeneration;
    public float currentPlayerLifeSteal;
    public int currentMeleeDamage;
    public int currentRangeDamage;
    public float currentElementalDamage;
    public float currentAttackSpeed;
    public float currentCritialDamagemultiple;
    public float currentAttackRange;





    void OnEnable()
    {
        currentMovementSpeed = playerStats.MovementSpeed;
        currentPlayerHP = playerStats.PlayerHP;
        currentPlayerArmor = playerStats.PlayerArmor;
        currentPlayerDodge = playerStats.PlayerDodge;
        currentPlayerCritChance = playerStats.PlayerCritChance;
        currentPlayerLuck = playerStats.PlayerLuck;
        currentPlayerHPRegeneration = playerStats.PlayerHPRegeneration;
        currentPlayerLifeSteal = playerStats.PlayerLifeSteal;
        currentMeleeDamage = playerStats.MeleeDamage;
        currentRangeDamage = playerStats.RangeDamage;
        currentElementalDamage = playerStats.ElementalDamage;
        currentAttackSpeed = playerStats.AttackSpeed;
        currentCritialDamagemultiple = playerStats.CritialDamagemultiple;
        currentAttackRange = playerStats.AttackRange;


     
    }

    public void UpdatePlayerStart(UpgradeCardData updateData)
    {
       
        for(int i = 0; i < updateData.attributes.Count; i++) {
            switch(updateData.attributes[i].attributeName)
            {
                case PlayerAtributeEnum.PlayerMovementSpeed:
                    {   if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentMovementSpeed += updateData.attributes[i].attributeValue/100;
                        }else
                        {
                            if (currentMovementSpeed - updateData.attributes[i].attributeValue >= 0)
                            {
                                currentMovementSpeed -= updateData.attributes[i].attributeValue;
                            }
                            else
                            {
                                currentMovementSpeed = 0;
                            }
                            
                        }                      
                        break;
                    }
                case PlayerAtributeEnum.PlayerMaxHp:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerHP += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            HealthBarController.Instance.ChangeMaxHp();
                        }
                        else
                        {   if(currentPlayerHP - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 10)
                            {
                                currentPlayerHP -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                                HealthBarController.Instance.ChangeMaxHp();
                            }
                            else
                            {
                                currentPlayerHP = 10;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerArmor:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerArmor += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if (currentPlayerArmor - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 0)
                            {
                                currentPlayerArmor -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerArmor = 0;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerDodge:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerDodge += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentPlayerDodge - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 0)
                            {
                                currentPlayerDodge -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerDodge = 0;
                            }
                           
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerCritialChance:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerCritChance += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentPlayerCritChance - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 0)
                            {
                                currentPlayerCritChance -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerCritChance = 0;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerLuck:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerLuck += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentPlayerLuck - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >=0)
                            {
                                currentPlayerLuck -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerLuck = 0;
                            }                           
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerHpRegentation:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerHPRegeneration += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if (currentPlayerHPRegeneration - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 0)
                            {
                                currentPlayerHPRegeneration -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerHPRegeneration = 0;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerLifesteal:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentPlayerLifeSteal += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentPlayerLifeSteal - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 0)
                            {
                                currentPlayerLifeSteal -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentPlayerLifeSteal = 0;
                            }
                           
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerMeleeDamage:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentMeleeDamage += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if (currentMeleeDamage - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 1)
                            {
                                currentMeleeDamage -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentMeleeDamage = 1;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerRangeDamage:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentRangeDamage += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentRangeDamage - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 1)
                            {
                                currentRangeDamage -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentRangeDamage = 1;
                            }
                           
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerElementalDamage:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentElementalDamage += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentElementalDamage - Mathf.RoundToInt(updateData.attributes[i].attributeValue) >= 1)
                            {
                                currentElementalDamage -= Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                            }
                            else
                            {
                                currentElementalDamage = 1;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerAttackSpeed:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentAttackSpeed += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if (currentAttackSpeed - updateData.attributes[i].attributeValue >= 0.05f)
                            {
                                currentAttackSpeed -= updateData.attributes[i].attributeValue;
                            }
                            else
                            {
                                currentAttackSpeed = 0.05f;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerCritialDamageMultiple:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentCritialDamagemultiple += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {   if(currentCritialDamagemultiple - updateData.attributes[i].attributeValue >= 1)
                            {
                                currentCritialDamagemultiple -= updateData.attributes[i].attributeValue;
                            }
                            else
                            {
                                currentCritialDamagemultiple = 1;
                            }
                            
                        }
                        break;
                    }
                case PlayerAtributeEnum.PlayerAttackRange:
                    {
                        if (updateData.attributes[i].atributeType == AtributeType.INCREASE)
                        {
                            currentAttackRange += Mathf.RoundToInt(updateData.attributes[i].attributeValue);
                        }
                        else
                        {
                            if (currentAttackRange - updateData.attributes[i].attributeValue >= 1)
                            {
                                currentAttackRange -= updateData.attributes[i].attributeValue;
                            }
                            else
                            {
                                currentAttackRange = 0;
                            }

                        }
                        break;
                    }
            }
        }
        
    }

}
