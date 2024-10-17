using UnityEngine;
using TMPro;

public class NormalChestController : MonoBehaviour
{
   private static TextMeshProUGUI normalChestTextInGame;

   


    public static int normalChestCount = 0;


    private void OnEnable()
    {
        Chest.OnCollectedNormalChest += OnCollectedNormalChest;
      
    }

    void Start()
    {
        normalChestCount = 0;

        normalChestTextInGame = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        UpdateChestText();
    }


    void Update()
    {

    }

    public void OnCollectedNormalChest()
    {
        normalChestCount += 1;
        UpdateChestText();
    }

    private static void UpdateChestText()
    {
        normalChestTextInGame.text = normalChestCount.ToString();
      
        
       

    }

    private void OnDisable()
    {
        Chest.OnCollectedNormalChest -= OnCollectedNormalChest;
    }

    public static void OpenedAChest()
    {   
        normalChestCount--;
        UpdateChestText();
    }
}
