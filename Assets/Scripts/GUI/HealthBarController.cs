using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : Singleton<HealthBarController>
{
    public Slider healthBar;
    public PlayerCurrentStats PlayerCurrentStats;
    public TextMeshProUGUI healthText;

    private void OnEnable()
    {
        PlayerHealth.HealthUpdate += UpdateHealthPoint;

    }
    void Start()
    {
        InitHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        PlayerHealth.HealthUpdate -= UpdateHealthPoint;
    }

    void UpdateHealthPoint(int heathPoint)
    {
        healthBar.value += heathPoint;
        healthText.text = $"{healthBar.value} / {healthBar.maxValue}";

    }

    public void ChangeMaxHp()
    {   
        healthBar.maxValue = PlayerCurrentStats.currentPlayerHP;
    }

    public void InitHP()
    {
        healthBar = GetComponent<Slider>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
       
        healthBar.maxValue = PlayerCurrentStats.currentPlayerHP;
        healthBar.value = PlayerCurrentStats.currentPlayerHP;
        healthText.text = $"{healthBar.value} / {healthBar.maxValue}";
    }
    


}
