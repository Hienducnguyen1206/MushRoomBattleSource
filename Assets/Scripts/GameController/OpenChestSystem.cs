using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestSystem : Singleton<OpenChestSystem>
{
    public GameObject CardZone;

    [SerializeField] List<UpgradeCardData> NormalUpdateCardList = new List<UpgradeCardData>();
    [SerializeField] List<UpgradeCardData> RareUpdateCardList = new List<UpgradeCardData>();
    [SerializeField] List<UpgradeCardData> EpicUpdateCardList = new List<UpgradeCardData>();
    [SerializeField] List<UpgradeCardData> LegendaryUpdateCardList = new List<UpgradeCardData>();
    [SerializeField] List<UpgradeCardData> LevelUpdateCardList = new List<UpgradeCardData>();


    [SerializeField] GameObject NormalCard;
    [SerializeField] GameObject RareCard;
    [SerializeField] GameObject EpicCard;
    [SerializeField] GameObject LegendaryCard;
    [SerializeField] GameObject LevelCard;

    [SerializeField] GameObject InventoryUpgradePrefab;
  


  
    public void DisplayCard(UpgradeCardData card)
    {
        GameObject newCard = null;
        switch (card.cardType)
        {
            case CardTypeEnum.NORMAL:
                {
                    newCard = Instantiate(NormalCard);
                    newCard.GetComponent<CardData>().upgradeCardData = card;
                    break;
                }
            case CardTypeEnum.RARE:
                {
                    newCard = Instantiate(RareCard);
                    newCard.GetComponent<CardData>().upgradeCardData = card;
                    break;
                }
            case CardTypeEnum.EPIC:
                {
                    newCard = Instantiate(EpicCard);
                    newCard.GetComponent<CardData>().upgradeCardData = card;
                    break;
                }
            case CardTypeEnum.LEGENDARY:
                {
                     newCard = Instantiate(LegendaryCard);
                    newCard.GetComponent<CardData>().upgradeCardData = card;
                    break;
                }
            case CardTypeEnum.LEVELUP:
                {
                    newCard = Instantiate(LevelCard);
                    newCard.GetComponent<CardData>().upgradeCardData = card;
                    break;
                }
        }
        

        TextMeshProUGUI cardName = newCard.GetComponentInChildren<TextMeshProUGUI>();
        Image cardAvatar = newCard.transform.Find("UpgradeImg/Upgrade").GetComponent<Image>();
        Transform thirdChild = newCard.transform.GetChild(3);
        cardName.text = card.cardName;
        cardName.fontSize = 24;
        RectTransform cardNameRectranform = cardName.GetComponent<RectTransform>();
        cardNameRectranform.sizeDelta = new Vector2(280, 25);
        cardAvatar.sprite = card.cardImage;
        TextMeshProUGUI reDrawCost = thirdChild.GetComponentInChildren<TextMeshProUGUI>();

        reDrawCost.text = card.ReDrawCost.ToString();

        Transform AttributeZone = newCard.transform.Find("UpgradeStats");
       
        for(int i = 0;i< card.attributes.Count; i++) {
            GameObject Attribute = new GameObject();
            Attribute.AddComponent<TextMeshProUGUI>();
            TextMeshProUGUI attribute = Attribute.GetComponent<TextMeshProUGUI>();
            if(card.attributes[i].atributeType == AtributeType.INCREASE) {
                attribute.text = Services.ConvertToReadableFormat( card.attributes[i].attributeName.ToString()) + " "+ "+"+ " " + card.attributes[i].attributeValue;
                attribute.color = Color.green;
            }
            else
            {
                attribute.text = Services.ConvertToReadableFormat(card.attributes[i].attributeName.ToString()) + " " + "-" + " " + card.attributes[i].attributeValue;
                attribute.color = Color.red;
            }
           

            RectTransform rectTransform = attribute.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 28);
            attribute.fontSize = 19;
            attribute.alignment = TextAlignmentOptions.Center;
            attribute.alignment = TextAlignmentOptions.Left;
            Attribute.transform.SetParent(AttributeZone,false);
        }



        newCard.transform.SetParent(CardZone.transform,false);
    }
    public  void Redraw(GameObject currentCard) {
        Transform thirdChild = currentCard.transform.GetChild(3);
        TextMeshProUGUI reDrawCost = thirdChild.GetComponentInChildren<TextMeshProUGUI>();
        UpgradeCardData newDrawCard = null;
        switch (reDrawCost.text)
        {
            case "1":
                {
                    newDrawCard = Services.GetRandomElement(NormalUpdateCardList);
                    break;
                }
            case "2":
                {
                    newDrawCard = Services.GetRandomElement(LevelUpdateCardList);
                    break;
                }
            case "3":
                {
                    newDrawCard = Services.GetRandomElement(RareUpdateCardList);
                    break;
                }
            case "4":
                {
                    newDrawCard = Services.GetRandomElement(EpicUpdateCardList);
                    break;
                }
            case "5":
                {
                    newDrawCard = Services.GetRandomElement(LegendaryUpdateCardList);
                    break;
                }
        }

        
        if(newDrawCard == null) {
            Debug.Log("newDrawCard is null"); return;
        }

       
        if(CoinsController.Instance.coinCount - newDrawCard.ReDrawCost >= 0)
        {
            CoinsController.Instance.ChangeNumberCoin(-newDrawCard.ReDrawCost);
        }
        else
        {
            Debug.Log("Not Enough coin");
            return;
        }
        




        TextMeshProUGUI cardName = currentCard.GetComponentInChildren<TextMeshProUGUI>();
        Image cardAvatar = currentCard.transform.Find("UpgradeImg/Upgrade").GetComponent<Image>();
        cardName.text = newDrawCard.cardName;
        cardAvatar.sprite =newDrawCard.cardImage;

        Transform AttributeZone = currentCard.transform.Find("UpgradeStats");


        ClearAllChildren(AttributeZone.gameObject);
        for (int i = 0; i < newDrawCard.attributes.Count; i++)
        {
            GameObject Attribute = new GameObject();
            Attribute.AddComponent<TextMeshProUGUI>();
            TextMeshProUGUI attribute = Attribute.GetComponent<TextMeshProUGUI>();
            if (newDrawCard.attributes[i].atributeType == AtributeType.INCREASE)
            {
                attribute.text = newDrawCard.attributes[i].attributeName + "+" + newDrawCard.attributes[i].attributeValue;
                attribute.color = Color.green;
            }
            else
            {
                attribute.text = newDrawCard.attributes[i].attributeName + "-" + newDrawCard.attributes[i].attributeValue;
                attribute.color = Color.red;
            }
            attribute.fontSize = 15;
            Attribute.transform.SetParent(AttributeZone, false);
        }

    }

    public void SelectACard(UpgradeCardData upgradeCardData) {

        PlayerCurrentStats.Instance.UpdatePlayerStart(upgradeCardData);
        UpgradeInventoryController.Instance.AddAUpgrade(upgradeCardData, InventoryUpgradePrefab);
        ClearAllChildren(CardZone);
        OpenChestMenuController.Instance.OpenChest();



    }

    public void ClearAllChildren(GameObject parent)
    {
        
        while (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }
    }

    public void ShowCard(CardTypeEnum cardTypeEnum)
    {
        switch (cardTypeEnum)
        {
            case CardTypeEnum.NORMAL:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        UpgradeCardData card = Services.GetRandomElement(NormalUpdateCardList);
                        DisplayCard(card);

                    }
                    break;
                }
            case CardTypeEnum.RARE:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        UpgradeCardData card = Services.GetRandomElement(RareUpdateCardList);
                        DisplayCard(card);

                    }
                    break;
                }
            case CardTypeEnum.EPIC:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        UpgradeCardData card = Services.GetRandomElement(EpicUpdateCardList);
                        DisplayCard(card);

                    }
                    break;
                }
            case CardTypeEnum.LEGENDARY:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        UpgradeCardData card = Services.GetRandomElement(LegendaryUpdateCardList);
                        DisplayCard(card);

                    }
                    break;
                }
            case CardTypeEnum.LEVELUP:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        UpgradeCardData card = Services.GetRandomElement(LevelUpdateCardList);
                        DisplayCard(card);

                    }
                    break;
                }
        }
    }



    

}
