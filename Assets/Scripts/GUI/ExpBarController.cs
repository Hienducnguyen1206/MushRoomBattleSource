using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarController : Singleton<ExpBarController>
{
    private Slider expBar;
    public GameObject level;
    public GameObject expNumber;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI expNumberText;
    public int currentPlayerLevel = 1;
    public static Action OnLevelUp;

    void OnEnable()
    {
        ExpGem.OnCollectedExp += UpdateExp;
    }

    void Start()
    {
        expBar = GetComponent<Slider>();
        expBar.value = 0;
        expBar.maxValue = LevelDesign.Instance.m_Players[currentPlayerLevel - 1].ExperienceRequired;

        levelText = level.GetComponent<TextMeshProUGUI>();
        expNumberText = expNumber.GetComponent<TextMeshProUGUI>();

        levelText.text = $"LV: {currentPlayerLevel}";
        expNumberText.text = $"{expBar.value} / {expBar.maxValue}";
    }

    void OnDisable()
    {
        ExpGem.OnCollectedExp -= UpdateExp;
    }

    void UpdateExp()
    {
        expBar.value += 1;
        // Cập nhật giá trị text của expNumberText
        expNumberText.text = $"{expBar.value} / {expBar.maxValue}";

        // Kiểm tra nếu người chơi đã đạt mức kinh nghiệm tối đa và cần lên cấp
        if (expBar.value >= expBar.maxValue)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        OnLevelUp.Invoke();
        currentPlayerLevel += 1;
        expBar.value = 0;
        expBar.maxValue = LevelDesign.Instance.m_Players[currentPlayerLevel - 1].ExperienceRequired;

        levelText.text = $"LV: {currentPlayerLevel}";
        expNumberText.text = $"{expBar.value} / {expBar.maxValue}";
    }
}
