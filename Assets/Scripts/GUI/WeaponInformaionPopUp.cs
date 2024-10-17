using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponInformaionPopUp : Singleton<WeaponInformaionPopUp>
{
    [SerializeField] List<GameObject> list = new List<GameObject>();
    TextMeshProUGUI weaponName;
    TextMeshProUGUI weaponStats;
    TextMeshProUGUI weaponDescription;

     public bool PopupisActive;
    void Start()
    {
        gameObject.SetActive(false);
        weaponName = gameObject.transform.Find("Content/Name").GetComponent<TextMeshProUGUI>();
        weaponStats = gameObject.transform.Find("Content/Stats").GetComponent<TextMeshProUGUI>();
        weaponDescription = gameObject.transform.Find("Content/Description").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopUp(ScriptableObject Data)
    {
        
        gameObject.SetActive(true);
     
        GunData gunData = Data as GunData;
        MagicStaffData magicStaffData = Data as MagicStaffData;
        SwordData swordData = Data as SwordData;
       switch (Data)
        {
            case GunData:
                {
                  weaponName.text = gunData.name;
                  weaponStats.text = "Attack Damage: " + gunData.attackDamage.ToString() + "\n" +
                                      "Attack Range:  " + gunData.attackRange.ToString() + "\n" +
                                      "Attack Per Second: " + gunData.attackPerSecond.ToString() + "/s" + "\n" +
                                      "Critial Chance: " + gunData.critChance.ToString() + "%" + "\n" +
                                      "Bullet Speed: " + gunData.bulletSpeed.ToString();

                    weaponDescription.text = gunData.Description;
                    break;
                }
            case MagicStaffData:
                { 
                   weaponName.text = magicStaffData.name;
                   weaponStats.text = "Attack Damage: " + magicStaffData.attackDamage.ToString() + "\n" +
                                      "Attack Range:  " + magicStaffData.attackRange.ToString() +  "\n" +
                                      "Attack Per Second: " + magicStaffData.attackPerSecond.ToString() + "/s" + "\n" +
                                      "Critial Chance: " + magicStaffData.critChance.ToString() + "%" +"\n" +
                                      "Magic Speed: " + magicStaffData.magicSpeed.ToString();

                    weaponDescription.text =magicStaffData.Description;
                    break;
                }
            case SwordData:
                {
                    weaponName.text = swordData.name;
                    weaponStats.text = "Attack Damage: " + swordData.attackDamage.ToString() + "\n" +
                                       "Attack Range:  " + swordData.attackRange.ToString() + "\n" +
                                       "Attack Per Second: " + ((0.1f/swordData.attackDuration)).ToString() + "/s" + "\n" +
                                       "Critial Chance: " + swordData.critChance.ToString() + "%" + "\n";
                    weaponDescription.text = swordData.Description;
                                      
                    break;
                }
        }
    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);
    }
}
