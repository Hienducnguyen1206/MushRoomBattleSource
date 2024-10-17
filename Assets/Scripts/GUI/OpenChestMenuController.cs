using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestMenuController : Singleton<OpenChestMenuController>
{
    private TextMeshProUGUI levelChestTextInMenu;
    private TextMeshProUGUI normalChestTextInMenu;
    private TextMeshProUGUI rareChestTextInMenu;
    private TextMeshProUGUI epicChestTextInMenu;
    private TextMeshProUGUI legendaryChestTextInMenu;

    Button button;

    private void Start()
    {
        button = gameObject.transform.GetChild(5).gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => OpenChestSystem.Instance.ClearAllChildren(OpenChestSystem.Instance.CardZone));
    }
    private void OnEnable()
    {
        OpenChest();            
    }

    public void GetAllCurrentChestAndShow()
    {

        GameObject levelChestUI = GameObject.Find("LevelChest");
        if (levelChestUI != null)
        {
            levelChestTextInMenu = levelChestUI.GetComponentInChildren<TextMeshProUGUI>();
            levelChestTextInMenu.text = LevelChestController.levelChestCount.ToString();
        }

        GameObject epicChestUI = GameObject.Find("EpicChest");
        if (epicChestUI != null)
        {
            epicChestTextInMenu = epicChestUI.GetComponentInChildren<TextMeshProUGUI>();
            epicChestTextInMenu.text = EpicChestController.epicChestCount.ToString();
        }

        GameObject normalChestUI = GameObject.Find("NormalChest");
        if (normalChestUI != null)
        {
            normalChestTextInMenu = normalChestUI.GetComponentInChildren<TextMeshProUGUI>();
            normalChestTextInMenu.text = NormalChestController.normalChestCount.ToString();
        }

        GameObject legendaryChestUI = GameObject.Find("LegendaryChest");
        if (legendaryChestUI != null)
        {
            legendaryChestTextInMenu = legendaryChestUI.GetComponentInChildren<TextMeshProUGUI>();
            legendaryChestTextInMenu.text = LegendaryChestController.legendaryChestCount.ToString();
        }

        GameObject rareChestUI = GameObject.Find("RareChest");
        if (rareChestUI != null)
        {
            rareChestTextInMenu = rareChestUI.GetComponentInChildren<TextMeshProUGUI>();
            rareChestTextInMenu.text = RareChestController.rareChestCount.ToString();
        }

    }



    public void OpenChest()
    {
        if (NormalChestController.normalChestCount > 0)
        {
            NormalChestController.OpenedAChest();
            GetAllCurrentChestAndShow();
            OpenChestSystem.Instance.ShowCard(CardTypeEnum.NORMAL);
        }
        else if (RareChestController.rareChestCount > 0)
        {
            RareChestController.OpenedAChest();
            GetAllCurrentChestAndShow();
            OpenChestSystem.Instance.ShowCard(CardTypeEnum.RARE);
        }
        else if (EpicChestController.epicChestCount > 0)
        {
            EpicChestController.OpenedAChest();
            GetAllCurrentChestAndShow();
            OpenChestSystem.Instance.ShowCard(CardTypeEnum.EPIC);
        }
        else if (LegendaryChestController.legendaryChestCount > 0)
        {
            LegendaryChestController.OpenedAChest();
            GetAllCurrentChestAndShow();
            OpenChestSystem.Instance.ShowCard(CardTypeEnum.LEGENDARY);
        }
        else if (LevelChestController.levelChestCount > 0)
        {
            LevelChestController.OpenedAChest();
            GetAllCurrentChestAndShow();
            OpenChestSystem.Instance.ShowCard(CardTypeEnum.LEVELUP);
        }
        else
        {
           GameController.Instance.OpenShopMenu();
            gameObject.SetActive(false);
        }
    }
}
