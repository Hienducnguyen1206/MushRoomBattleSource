using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{   [SerializeField]
    MonsterData monsterData;
    [SerializeField]
    BulletType BulletType;
    Vector2 contactPoint = Vector2.zero;
    void Start()
    {
        
    }

   
   

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider != null)
        {
            
            IHealthPoint hp = collider.gameObject.GetComponent<IHealthPoint>();
            IBloodEffect ef = collider.gameObject.GetComponent<IBloodEffect>();
            contactPoint = collider.ClosestPoint(transform.position);
            if (hp!= null)
            {
                hp.TakeDamage(monsterData.AttackDamage, false, DamageType.magic, contactPoint);
                BulletPoolManager.Instance.ReturnBulletToPool(gameObject, BulletType);
            }

           
        }
    }

    private void Update()
    {
        if (transform.position.sqrMagnitude > 500f)
        {
            BulletPoolManager.Instance.ReturnBulletToPool(gameObject, BulletType);
        }
    }
       
    
}
