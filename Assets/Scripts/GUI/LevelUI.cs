using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    TextMeshProUGUI levelText;
    private void OnEnable()
    {
        levelText = gameObject.GetComponent<TextMeshProUGUI>();
        levelText.text = "Level:" + " " + ExpBarController.Instance.currentPlayerLevel.ToString();
    }
}
