using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ShopSystem : Singleton<ShopSystem>
{
    [SerializeField]  List<UpgradeCardData> upgradeCardsLists = new List<UpgradeCardData>();
    [SerializeField]  List<WeaponCard> weaponCardList= new List<WeaponCard>();

    [SerializeField] GameObject ShopCardPrefab;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject InventoryUpgradePrefab;

    Vector3 weaponScale = new Vector3(1f, 0.32f, 1f);
    Vector3 weaponRoration = new Vector3(0,0,40f);

    Color NormalColor = new Color(1,1,1,1);
    Color RareColor = new Color(20f / 255f, 228f / 255f, 251f / 255f, 255f / 255f);
    Color LevelColor = new Color(136f/ 255f,242f / 255f,114f / 255f,255f / 255f);
    Color EpicColor = new Color(232f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
    Color LegendaryColor = new Color(255f / 255f, 247f / 255f, 0f / 255f, 255f / 255f);

    Color WeaponColor = new Color(244f / 255f, 126f / 255f, 21f / 255f, 255f / 255f);



    public void DisplayShopCard()
    {
        // Đếm số card đang bị khóa (không xóa chúng)
        int lockedCardCount = 0;

        foreach (Transform child in Shop.transform)
        {
            CardStatus cardStatus = child.gameObject.GetComponent<CardStatus>();

            if (cardStatus != null && cardStatus.Locked && cardStatus.SoldOut == false)
            {
                lockedCardCount++;
                continue; 
            }
            Destroy(child.gameObject); 
        }

        
        int cardsToCreate = 10 - lockedCardCount;

        for (int i = 0; i < cardsToCreate; i++)
        {
            int type = Random.Range(0, 2);
            GameObject Shopcard = Instantiate(ShopCardPrefab, Vector3.zero, Quaternion.identity);
            CardStatus cardStatus = Shopcard.GetComponent<CardStatus>();
            Shopcard.transform.SetParent(Shop.transform, false);

            TextMeshProUGUI ShopCardName = Shopcard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Image ShopcardImage = Shopcard.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            Image ShopcardOutLine = Shopcard.transform.GetChild(1).GetChild(1).GetComponent<Image>();

            Transform AttributeZone = Shopcard.transform.GetChild(2);

            RectTransform ShopcardImageRect = Shopcard.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
            TextMeshProUGUI ShopcardCoinText = Shopcard.transform.GetChild(3).GetChild(1).GetComponentInChildren<TextMeshProUGUI>();

            Button btn = Shopcard.transform.GetChild(4).GetComponentInChildren<Button>();
            GameObject Soldout = Shopcard.transform.GetChild(5).gameObject;

            switch (type)
            {
                case 0:
                    {
                        UpgradeCardData card = Services.GetRandomElement(upgradeCardsLists);
                        ShopCardName.text = card.name;
                        ShopcardImage.sprite = card.cardImage;
                        ShopcardCoinText.text = card.BuyCost.ToString();

                        UpgradeCardData localCard = card;


                        for (int j = 0; j < card.attributes.Count; j++)
                        {
                            GameObject Attribute = new GameObject();
                            Attribute.AddComponent<TextMeshProUGUI>();
                            TextMeshProUGUI attribute = Attribute.GetComponent<TextMeshProUGUI>();
                            if (card.attributes[j].atributeType == AtributeType.INCREASE)
                            {
                                attribute.text = Services.ConvertToReadableFormat(card.attributes[j].attributeName.ToString()) + " " + "+" + " " + card.attributes[j].attributeValue;
                                attribute.color = Color.green;
                            }
                            else
                            {
                                attribute.text = Services.ConvertToReadableFormat(card.attributes[j].attributeName.ToString()) +  " " + card.attributes[j].attributeValue;
                                attribute.color = Color.red;
                            }


                            RectTransform rectTransform = attribute.GetComponent<RectTransform>();
                            rectTransform.sizeDelta = new Vector2(280, 20);
                            attribute.fontSize = 13;
                            attribute.alignment = TextAlignmentOptions.Center;
                            attribute.alignment = TextAlignmentOptions.Left;
                            Attribute.transform.SetParent(AttributeZone, false);
                        }



                        switch (card.cardType)
                        {
                            case CardTypeEnum.NORMAL:
                            {
                                    ShopcardOutLine.color = NormalColor;
                                    ShopCardName.color = NormalColor;
                                    break;
                            }
                            case CardTypeEnum.LEVELUP:
                                {
                                    ShopcardOutLine.color = LevelColor;
                                    ShopCardName.color = LevelColor;
                                    break;
                                }
                            case CardTypeEnum.RARE:
                                {
                                    ShopcardOutLine.color = RareColor;
                                    ShopCardName.color= RareColor;
                                    break;
                                }
                            case CardTypeEnum.EPIC:
                                {
                                    ShopcardOutLine.color = EpicColor;
                                    ShopCardName.color = EpicColor;
                                    break;
                                }
                            case CardTypeEnum.LEGENDARY:
                                {
                                    ShopcardOutLine.color = LegendaryColor;
                                    ShopCardName.color = LegendaryColor;
                                    break;
                                }
                        }




                        btn.onClick.AddListener(() => {

                            if(CoinsController.Instance.coinCount - localCard.BuyCost >= 0)
                            {
                                cardStatus.SoldOut = true;
                                CoinsController.Instance.ChangeNumberCoin(-localCard.BuyCost);
                                PlayerCurrentStats.Instance.UpdatePlayerStart(localCard);
                                UpgradeInventoryController.Instance.AddAUpgrade(localCard, InventoryUpgradePrefab);
                                Soldout.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Not Enough Coin");
                            }
                           

                        });
                     
                        
                        break;
                    }
                case 1:
                    {
                        WeaponCard card = Services.GetRandomElement(weaponCardList);
                        ShopCardName.text = card.name;
                        ShopcardImage.sprite = card.cardImage;
                        ShopcardCoinText.text = card.Cost.ToString();
                        ShopcardImageRect.localScale = weaponScale;
                        ShopcardImageRect.rotation = Quaternion.Euler(weaponRoration);

                        WeaponCard localCard = card;

                        ShopcardOutLine.color = WeaponColor;
                        ShopCardName.color = WeaponColor;
                        btn.onClick.AddListener(() => {

                            if (CoinsController.Instance.coinCount - localCard.Cost >= 0)
                            {
                                cardStatus.SoldOut = true;
                                CoinsController.Instance.ChangeNumberCoin(-localCard.Cost);
                                WeaponInventoryController.Instance.AddItemToStore(localCard.name.Replace(" ", ""));                               
                                Soldout.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Not Enough Coin");
                            }


                        });

                       
                        break;
                    }
            }
        }
    }



}
