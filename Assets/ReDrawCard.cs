using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReDrawCard : MonoBehaviour
{
    private OpenChestSystem openChestSystem;

    private void OnEnable()
    {
        GameObject gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            openChestSystem = gameController.GetComponent<OpenChestSystem>();
            if (openChestSystem == null)
            {
                Debug.LogError("OpenChestSystem không được tìm thấy trên GameController.");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy GameController.");
        }

        Button button = gameObject.GetComponentInChildren<Button>();
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => openChestSystem.Redraw(gameObject.transform.parent.gameObject));
    }

    
}
