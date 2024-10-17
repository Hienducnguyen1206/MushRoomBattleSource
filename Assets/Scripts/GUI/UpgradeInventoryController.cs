using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInventoryController : Singleton<UpgradeInventoryController>
{   

    Color RareColor = new Color(20f / 255f, 88f / 255f, 251f / 255f, 255f / 255f);     
    Color EpicColor = new Color(232f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
    Color LegendaryColor = new Color(255f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);  
    Color LevelColor = new Color(136f / 255f, 242f / 255f, 114f / 255f, 255f / 255f);

    public void AddAUpgrade(UpgradeCardData upgradeCardData, GameObject UpgradePrefabs)
    {
        GameObject upgrade = Instantiate(UpgradePrefabs);
        upgrade.transform.SetParent(gameObject.transform, false);
        upgrade.transform.Find("UpgradeImage").GetComponent<Image>().sprite = upgradeCardData.cardImage;

        Image outline = upgrade.transform.Find("OutLine").GetComponent<Image>();
        if (outline != null)
        {
            if (upgradeCardData.cardType == CardTypeEnum.RARE)
            {
                outline.color = RareColor;
            }
            else if (upgradeCardData.cardType == CardTypeEnum.EPIC)
            {
                outline.color = EpicColor;
            }
            else if (upgradeCardData.cardType == CardTypeEnum.LEGENDARY)
            {
                outline.color = LegendaryColor;
            }
            else if (upgradeCardData.cardType == CardTypeEnum.LEVELUP)
            {
                outline.color = LevelColor;
            }
        }
        else
        {
            Debug.LogError("OutLine not found on UpgradePrefabs!");
        }
    }
}
