using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EpicChestController : MonoBehaviour
{
    private static TextMeshProUGUI epicChestTextInGame;
   

    public static  int epicChestCount = 0;


    private void OnEnable()
    {
        Chest.OnCollectedEpicChest += OnCollectedEpicChest;
    }

    void Start()
    {
        epicChestCount = 0;
        epicChestTextInGame = GetComponentInChildren<TextMeshProUGUI>();
        UpdateChestText();
    }


    void Update()
    {

    }

    public void OnCollectedEpicChest()
    {
        epicChestCount += 1;
        UpdateChestText();
    }

    private static void UpdateChestText()
    {
        epicChestTextInGame.text = epicChestCount.ToString();

       
    }

    private void OnDisable()
    {
        Chest.OnCollectedEpicChest -= OnCollectedEpicChest;
    }

    public static void OpenedAChest()
    {
        epicChestCount--;
        UpdateChestText();
    }
}
