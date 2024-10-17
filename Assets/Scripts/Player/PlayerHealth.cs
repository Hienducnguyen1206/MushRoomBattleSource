using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth> , IHealthPoint
{
    public PlayerCurrentStats playerCurrentStats;
    float currentHp;
    [SerializeField] EffectType DodgePopUp;

    public static event Action<int> HealthUpdate;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = playerCurrentStats.currentPlayerHP;
      
    }

    // Update is called once per frame

    private void Update()
    {
        CheckDead();
    }

    public void TakeDamage(int damage, bool critial, DamageType type, Vector3 hurtPosition)
    {
        bool CanDodge = Services.Chance(PlayerCurrentStats.Instance.currentPlayerDodge);
        if(CanDodge)
        {
            GameObject PopUp = EffectPoolManager.Instance.GetEffect(DodgePopUp);
            PopUp.transform.position = hurtPosition;
            StartCoroutine(SetActiveFalse(PopUp,DodgePopUp));
            return;
           
        }
        else
        {   
            
            currentHp -= damage;
           
            HealthUpdate?.Invoke(-damage);
        }
       
       
    }

    public void CheckDead()
    {
        if (currentHp <1)
        {   
            GameController.Instance.StopAllCoroutines();
            GameController.Instance.GameOver();
        }
    }


  
        IEnumerator RegenHP(float hp)
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                currentHp += hp;
                HealthUpdate?.Invoke((int)hp);
            }

        }

    public void StartRegenHP()
    {
        StartCoroutine(RegenHP(playerCurrentStats.currentPlayerHPRegeneration));
    }

    public void StopRegenHP()
    {
        StopCoroutine(RegenHP(playerCurrentStats.currentPlayerHPRegeneration));
    }

    IEnumerator SetActiveFalse(GameObject go, EffectType effect)
    {
        yield return new WaitForSeconds(2f);
        if (go != null)
        {
            EffectPoolManager.Instance.ReturnEffectToPool(go, effect);
        }
    }

    public void Healing(float HP)
    {
        currentHp += HP;
        HealthUpdate.Invoke((int)HP);
    }

    
}
