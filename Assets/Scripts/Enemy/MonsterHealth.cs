using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MonsterHealth : MonoBehaviour,IHealthPoint,IBloodEffect
{
    [SerializeField] MonsterData monsterData;
    MonsterStats monsterStats;
    public int maxHp;
    public int currentHp;
    public bool isDead = false;
  
    public EffectType[] bloodeffect;
    public EffectType normalPhysicDamagePopUp;
    public EffectType critialPhysicDamagePopUp;
    public EffectType normalMagicDamagePopUp;
    public EffectType critialMagicDamagePopUp;
 

    public event Action MonsterIsDead;
    // Start is called before the first frame update
    void Start()
    {

        maxHp = monsterStats.startHP;
        currentHp = maxHp;

    }

   

   

    public void TakeDamage(int damage,bool critial, DamageType type, Vector3 hurtPosition)
    {
        if (!isDead)
        {
            if (type == DamageType.physic)
            {
                damage = ((damage - monsterStats.currentArmor)< 0)? 0: damage - monsterStats.currentArmor;
                if (!critial)
                {
                    GameObject PopUp = EffectPoolManager.Instance.GetEffect(normalPhysicDamagePopUp);
                    PopUp.transform.position = hurtPosition;


                    TextMeshPro damageText = PopUp.GetComponentInChildren<TextMeshPro>();
                    damageText.text = damage.ToString();
                    PlayerHealth.Instance.Healing(damage * PlayerCurrentStats.Instance.currentPlayerLifeSteal/1000);
                    StartCoroutine(SetActiveFalse(PopUp, normalPhysicDamagePopUp));
                }
                else
                {
                    GameObject PopUp = EffectPoolManager.Instance.GetEffect(critialPhysicDamagePopUp);
                    PopUp.transform.position = hurtPosition;


                    TextMeshPro damageText = PopUp.GetComponentInChildren<TextMeshPro>();
                    damageText.text = damage.ToString();
                    PlayerHealth.Instance.Healing(damage * PlayerCurrentStats.Instance.currentPlayerLifeSteal/1000);
                    StartCoroutine(SetActiveFalse(PopUp, critialPhysicDamagePopUp));
                }

                currentHp = currentHp - damage;


            }
            else if (type == DamageType.magic)
            {

                damage = ((damage - monsterStats.currentMagicResistance)<0)?0: (damage - monsterStats.currentMagicResistance);
                if (!critial)
                {
                    GameObject PopUp = EffectPoolManager.Instance.GetEffect(normalMagicDamagePopUp);
                    PopUp.transform.position = hurtPosition;


                    TextMeshPro damageText = PopUp.GetComponentInChildren<TextMeshPro>();
                    damageText.text = damage.ToString();

                    StartCoroutine(SetActiveFalse(PopUp, normalMagicDamagePopUp));
                }
                else
                {
                    GameObject PopUp = EffectPoolManager.Instance.GetEffect(critialMagicDamagePopUp);
                    PopUp.transform.position = hurtPosition;


                    TextMeshPro damageText = PopUp.GetComponentInChildren<TextMeshPro>();
                    damageText.text = damage.ToString();
                    StartCoroutine(SetActiveFalse(PopUp, critialMagicDamagePopUp));
                }

                currentHp = currentHp - damage;
            }


            if (currentHp <= 0)
            {
                isDead = true;
                MonsterIsDead?.Invoke();
            }
        }
      
    }

    void StartMonsterDeadAction()
    {   
        if(!isDead)
        {
            currentHp = 0;
            isDead = true;
            MonsterIsDead?.Invoke();
        }
        
    }

    public void BloodEffect(Vector3 position)
    {  
       
        EffectType bloodtospawn = Services.RandomObject(bloodeffect);
        GameObject blood = EffectPoolManager.Instance.GetEffect(bloodtospawn);
        blood.transform.position = position;
        blood.transform.rotation = Quaternion.identity;
        StartCoroutine(SetActiveFalse(blood,bloodtospawn));
    }

    IEnumerator SetActiveFalse(GameObject go,EffectType effect)
    {
        yield return new WaitForSeconds(1f);
        if (go != null)
        {
            EffectPoolManager.Instance.ReturnEffectToPool(go, effect);
        }
    }

    void OnEnable()
    {
        isDead = false;
        GameController.KillAllMonster += StartMonsterDeadAction;
        monsterStats = gameObject.GetComponent<MonsterStats>();
        if (monsterStats == null)
        {
            Debug.LogError("MonsterStats component not found on the GameObject.");
            return;
        }
        maxHp = monsterStats.startHP;
        currentHp = maxHp;
    }

    
    void OnDisable()
    {
        GameController.KillAllMonster -= StartMonsterDeadAction;
    }
}
