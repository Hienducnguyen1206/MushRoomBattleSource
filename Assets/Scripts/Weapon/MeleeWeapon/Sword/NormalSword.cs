using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalSword : Sword
{
   
   
    public LayerMask enemyLayer;

    public PlayerStats playerStart;
    public SwordData normalSwordData;
    public Transform damagePoint;
   

    


    // Start is called before the first frame update
    new void Start()
    {
        StartCoroutine(AttackAfterDuration(1f));       
        base.Start();
    }

    // Update is called once per frame

    void Update()
    {
         FindNearestEnemyandRotation(playerTransform, normalSwordData.attackRange*2/7f , enemyLayer);
        
    }

    public override void Attack()
    {   
       
        StartCoroutine(MoveToAttact(normalSwordData));
        

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        inEnemyCollider = true;
        if (isAttacking && isAttacked && inEnemyCollider)
        {
            var hp = collider.gameObject.GetComponent<IHealthPoint>();
            var ef = collider.gameObject.GetComponent<IBloodEffect>();

            if (hp != null)
            {              
                // Kiểm tra đòn chí mạng
                bool critical = Services.Chance(normalSwordData.critChance + playerStart.PlayerCritChance);
                // Hiệu ứng máu tại điểm va chạm
                ef.BloodEffect(HurtPoint);

                // Tính toán sát thương
                int damage = (critical) ? Mathf.FloorToInt((normalSwordData.attackDamage + playerStart.RangeDamage) * playerStart.CritialDamagemultiple) :
                                          Mathf.FloorToInt((normalSwordData.attackDamage + playerStart.RangeDamage));

                // Gây sát thương
                hp.TakeDamage(damage, critical, DamageType.physic,HurtPoint);
                isAttacked = false;
            }           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inEnemyCollider = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 7f);
     
    }

   
}
