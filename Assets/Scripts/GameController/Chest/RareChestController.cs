using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RareChestController : MonoBehaviour
{
    private static TextMeshProUGUI rareChestTextInGame;
   
    public static int rareChestCount = 0;


    private void OnEnable()
    {
        Chest.OnCollectedRareChest += OnCollectedRareChest;
    }

    void Start()
    {
        rareChestCount = 0;
        rareChestTextInGame = GetComponentInChildren<TextMeshProUGUI>();
        UpdateChestText();
    }


    void Update()
    {

    }

    public void OnCollectedRareChest()
    {
        rareChestCount += 1;
        UpdateChestText();
    }

    private static void UpdateChestText()
    {
        rareChestTextInGame.text = rareChestCount.ToString();

       
    }

    private void OnDisable()
    {
        Chest.OnCollectedRareChest -= OnCollectedRareChest;
    }

    public static void OpenedAChest()
    {
        rareChestCount--;
        UpdateChestText();
    }
}
