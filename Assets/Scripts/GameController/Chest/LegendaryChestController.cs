using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LegendaryChestController : MonoBehaviour
{
    private static TextMeshProUGUI legendaryChestTextInGame;
  
    public static int legendaryChestCount = 0;


    private void OnEnable()
    {
        Chest.OnCollectedLegendaryChest += OnCollectedLegendaryChest;
    }

    void Start()
    {
        legendaryChestCount = 0;
        legendaryChestTextInGame = GetComponentInChildren<TextMeshProUGUI>();
        UpdateLegendaryChestText();
    }


    void Update()
    {

    }

    public void OnCollectedLegendaryChest()
    {
        legendaryChestCount += 1;
        UpdateLegendaryChestText();
    }

    private static void UpdateLegendaryChestText()
    {
        legendaryChestTextInGame.text = legendaryChestCount.ToString();

        
    }

    private void OnDisable()
    {
        Chest.OnCollectedLegendaryChest -= OnCollectedLegendaryChest;
    }

    public static void OpenedAChest()
    {
        legendaryChestCount--;
        UpdateLegendaryChestText();
    }
}
