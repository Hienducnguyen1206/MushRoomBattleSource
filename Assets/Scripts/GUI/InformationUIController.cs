using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationUIController : MonoBehaviour
{
    TextMeshProUGUI maxHp;
    TextMeshProUGUI armor;
    TextMeshProUGUI meleeDamage;
    TextMeshProUGUI rangeDamage;
    TextMeshProUGUI elementalDamage;
    TextMeshProUGUI luck;
    TextMeshProUGUI dodge;
    TextMeshProUGUI lifeSteal;
    TextMeshProUGUI moveSpeed;
    TextMeshProUGUI hpRegen;
    TextMeshProUGUI critialChanceBonus;
    TextMeshProUGUI critialDamageMultiple;
    TextMeshProUGUI attackRangeBonus;
    TextMeshProUGUI attackSpeedBonus; 
    private void OnEnable()
    {   
        maxHp = gameObject.transform.Find("MaxHP").gameObject.GetComponent<TextMeshProUGUI>();
        armor = gameObject.transform.Find("Armor").gameObject.GetComponent<TextMeshProUGUI>();
        meleeDamage = gameObject.transform.Find("MeleeDamage").gameObject.GetComponent<TextMeshProUGUI>();
        rangeDamage = gameObject.transform.Find("RangeDamage").gameObject.GetComponent<TextMeshProUGUI>();
        elementalDamage = gameObject.transform.Find("ElementalDamage").gameObject.GetComponent<TextMeshProUGUI>();
        luck = gameObject.transform.Find("Luck").gameObject.GetComponent<TextMeshProUGUI>();
        dodge = gameObject.transform.Find("Dodge").gameObject.GetComponent<TextMeshProUGUI>();
        lifeSteal = gameObject.transform.Find("LifeSteal").gameObject.GetComponent<TextMeshProUGUI>();
        moveSpeed = gameObject.transform.Find("MoveSpeed").gameObject.GetComponent<TextMeshProUGUI>();
        hpRegen = gameObject.transform.Find("HPRegen").gameObject.GetComponent<TextMeshProUGUI>();
        critialChanceBonus = gameObject.transform.Find("CritChance").gameObject.GetComponent<TextMeshProUGUI>();
        critialDamageMultiple = gameObject.transform.Find("CritDamageMultiple").gameObject.GetComponent<TextMeshProUGUI>();
        attackRangeBonus = gameObject.transform.Find("AttackRangeBonus").gameObject.GetComponent<TextMeshProUGUI>();
        attackSpeedBonus = gameObject.transform.Find("AttackSpeedBonus").gameObject.GetComponent<TextMeshProUGUI>();


        maxHp.text = "MaxHp:" + " " + PlayerCurrentStats.Instance.currentPlayerHP.ToString();
        armor.text = "Armor:" + " " + PlayerCurrentStats.Instance.currentPlayerArmor.ToString();
        meleeDamage.text = "Melee"+" "+"Damage:"+ " "+ "+" + PlayerCurrentStats.Instance.currentMeleeDamage.ToString();
        rangeDamage.text = "Range" + " " + "Damage:" + " " + "+" + PlayerCurrentStats.Instance.currentRangeDamage.ToString();
        elementalDamage.text = "Elemental" + " " + "Damage:" + " " + "+" + PlayerCurrentStats.Instance.currentElementalDamage.ToString();
        luck.text = "Luck:" + " " +  PlayerCurrentStats.Instance.currentPlayerLuck.ToString();
        dodge.text = "Dodge: " + " " + PlayerCurrentStats.Instance.currentPlayerDodge.ToString() + "%";
        lifeSteal.text = "Life"+" "+ "Steal:"+ " " + PlayerCurrentStats.Instance.currentPlayerLifeSteal.ToString()+ "%";
        moveSpeed.text = "Move"+" "+"Speed:"+" "+PlayerCurrentStats.Instance.currentMovementSpeed.ToString();
        hpRegen.text = "HP" + " " + "Regentation:"+ " "+ PlayerCurrentStats.Instance.currentPlayerHPRegeneration.ToString() + "/3s";
        critialChanceBonus.text = "Critial"+" " + " Chance "+""+" Bonus:" + " " + PlayerCurrentStats.Instance.currentPlayerCritChance.ToString() + "%";
        critialDamageMultiple.text = "Critial" + " " + " Damage" + " " + "Multiple:" + " " + PlayerCurrentStats.Instance.currentCritialDamagemultiple.ToString();
        attackRangeBonus.text = "Attack" + " " + "Range" + " " + " Bonus:" + " " +PlayerCurrentStats.Instance.currentAttackRange.ToString() + "%";
        attackSpeedBonus.text = "Attack" + " " + "Speed" + " " + "Bonus" + " " +PlayerCurrentStats.Instance.currentAttackSpeed.ToString() + "%";
    }
   
}
