using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectCountDown : MonoBehaviour
{
    float currenttime;
    float maxTime = 5;
    TextMeshProUGUI countdown;
    public EffectType effectType;
    float fillAmount;
    Image image;
    private void OnEnable()
    {
        currenttime = maxTime;
    }

    void Start()
    {  
        countdown = GetComponentInChildren<TextMeshProUGUI>();
         image = gameObject.transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currenttime -= Time.deltaTime;
        countdown.text = ((int) currenttime).ToString();
        
        image.fillAmount = ((maxTime - currenttime)/maxTime);
      
        if(currenttime < 0)
        {
            EffectPoolManager.Instance.ReturnEffectToPool(gameObject, effectType);
        }
    }
}
