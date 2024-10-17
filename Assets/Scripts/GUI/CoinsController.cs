using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinsController : Singleton<CoinsController>
{
    [SerializeField]  TextMeshProUGUI coinTextIngame;
    [SerializeField]  TextMeshProUGUI coinTextInOpenChestMenu;
    [SerializeField]  TextMeshProUGUI coinTextInNextWaveMenu;
    [SerializeField]  TextMeshProUGUI coinTextInShopMenu;

    public  int  coinCount = 0;


    private void OnEnable()
    {
        Coin.OnCollectedCoin += OnCollectedCoin;
    }

    void Start()
    {

        coinCount = 0;
        UpdateCoinText();
    }


    void Update()
    {
       
    }

    public void OnCollectedCoin()
    {
        coinCount += 1;
        UpdateCoinText();
    }

    public void ChangeNumberCoin(int coin)
    {
        coinCount += coin;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinTextIngame.text = coinCount.ToString();
        coinTextInOpenChestMenu.text = coinCount.ToString();
        coinTextInNextWaveMenu.text = coinCount.ToString();
        coinTextInShopMenu.text = coinCount.ToString();
    }

    private void OnDisable()
    {
        Coin.OnCollectedCoin -= OnCollectedCoin;
    }
}
