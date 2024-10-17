using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStaff : MagicStaff
{
    public MagicStaffData waterStaffData;
    public BulletType waterBullet;
    public LayerMask enemyLayer;
    public Transform firePoint;
    Vector3 baseScale;
   

    // Start is called before the first frame update
    public override void Start()
    {
        StartCoroutine(AttackAfterDuration(waterStaffData.attackPerSecond));
        baseScale = transform.localScale;
        base.Start();
    }

    public override void Attack()
    {
        Attacked = true;
        GameObject bullet = BulletPoolManager.Instance.GetBullet(waterBullet);
        bullet.transform.rotation = firePoint.rotation;
        bullet.transform.position = firePoint.position;


        if (bullet == null)
        {
            Debug.Log("No bullet");
            return;
        }

        // Thiết lập vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bullet.transform.right * waterStaffData.magicSpeed;


    }

    // Update is called once per frame
    void Update()
    {
         FindNearestEnemyandRotation(playerTransform, waterStaffData.attackRange + waterStaffData.attackRange * PlayerCurrentStats.Instance.currentAttackRange / 100, enemyLayer);
        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, waterStaffData.attackRange);
    }
    private void OnDrawGizmos()
    {

        // Thiết lập màu cho đường thẳng
        Gizmos.color = Color.red;

       
    }

  
}
