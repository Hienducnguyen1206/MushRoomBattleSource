using UnityEngine;
using TMPro;

public class LevelChestController : MonoBehaviour
{
    private static TextMeshProUGUI levelChestTextInGame;
   
    public static int levelChestCount = 0;


    private void OnEnable()
    {
        ExpBarController.OnLevelUp += OnLevelUp;
       
    }

    void Start()
    {

        levelChestTextInGame = GetComponentInChildren<TextMeshProUGUI>();
        levelChestCount = 0;
        UpdateChestText();
    }


    public static void OnLevelUp()
    {
        levelChestCount += 1;
        UpdateChestText();
    }

    private static void UpdateChestText()
    {
        levelChestTextInGame.text = levelChestCount.ToString();

    }

    private void OnDisable()
    {
        ExpBarController.OnLevelUp -= OnLevelUp;
    }

    public static void OpenedAChest()
    {
        levelChestCount--;
        UpdateChestText();
    }
}
