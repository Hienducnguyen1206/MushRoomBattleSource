using System.Collections;
using UnityEngine;

public class Laser : Gun
{
    public GunData laserGunData;
    public BulletType laserBullet;
    public LayerMask enemyLayer;
    public Transform firePoint;

    Vector3 baseScale;
   



    new void Start()
    {
        base.Start();
        StartCoroutine(AttackAfterDuration(laserGunData.attackPerSecond));
        baseScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        FindNearestEnemyandRotation(playerTransform, laserGunData.attackRange + laserGunData.attackRange * PlayerCurrentStats.Instance.currentAttackRange/100, enemyLayer);
            
    }

    public override void Attack()
    {
        Attacked = true;
        GameObject bullet = BulletPoolManager.Instance.GetBullet(laserBullet);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        if (bullet == null)
        {
            Debug.Log("No bullet");
            return;
        }

        // Thiết lập vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bullet.transform.right * laserGunData.bulletSpeed;

        // Bắt đầu hiệu ứng giật
        StartCoroutine(Recoil(laserGunData.recoilDistance, laserGunData.recoilDuration, firePoint));
    }

    
}