using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    private MonsterHealth MonsterHealth;
    float MonsterScaleX;
    Vector3 healthBarFlipX;
    Vector3 healthBarNonFlip;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        MonsterHealth = GetComponent<MonsterHealth>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.minValue = 0;
        healthBar.maxValue = MonsterHealth.maxHp;

        healthBarFlipX = new Vector3(-healthBar.gameObject.transform.localScale.x, healthBar.gameObject.transform.localScale.y, healthBar.gameObject.transform.localScale.z);
        healthBarNonFlip = new Vector3(healthBar.gameObject.transform.localScale.x, healthBar.gameObject.transform.localScale.y, healthBar.gameObject.transform.localScale.z);
        rect = healthBar.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = MonsterHealth.currentHp;
        IgnoreFlipX();
    }

    public void IgnoreFlipX()
    {   
        MonsterScaleX = gameObject.transform.localScale.x;
        if( MonsterScaleX < 0)
        {
            rect.localScale = healthBarFlipX;
        }else
        {
            rect.localScale = healthBarNonFlip;
        }
    }

   
}
