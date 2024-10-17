using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AddStatusEffect : MonoBehaviour
{
    public EffectType StatusEffect;
    void Start()
    { 
     
     
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            AddStatus();          
        }
    }

    public void AddStatus()
    {
        GameObject Status = EffectPoolManager.Instance.GetEffect(StatusEffect);
        Status.name = "Stun";
        Status.transform.SetParent(gameObject.transform);
        RectTransform rectTransform = Status.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        Image Icon = Status.transform.GetChild(0).GetComponent<Image>();
        Icon.sprite = DebuffBuffIcon.Instance.StatusIconDictionary["Stun"];
        Image FillIcon = Status.transform.GetChild(1).GetComponent<Image>();
        FillIcon.sprite = DebuffBuffIcon.Instance.StatusIconDictionary["Stun"];
        
    }
}
