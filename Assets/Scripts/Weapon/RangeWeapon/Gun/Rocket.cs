using System.Collections;
using UnityEngine;

public class Rocket : Gun
{
    public GunData rocketData;
    public BulletType rocket;
    public LayerMask enemyLayer;
    public Transform firePoint;
 
    Vector3 baseScale;




    public ParticleSystem TailRocketMuzzle;
    public ParticleSystem FrontRocketMuzzle;
    new void Start()
    {
        base.Start();
        StartCoroutine(AttackAfterDuration(rocketData.attackPerSecond));
        
        baseScale = transform.localScale;
    }

    void Update()
    {
        FindNearestEnemyandRotation(playerTransform, rocketData.attackRange + rocketData.attackRange* PlayerCurrentStats.Instance.currentAttackRange / 100, enemyLayer);
            
    }
    public override void Attack()
    {
        Attacked = true;
        FrontRocketMuzzle.transform.rotation = transform.rotation;
        FrontRocketMuzzle.Play();
        TailRocketMuzzle.transform.rotation = transform.rotation;
        TailRocketMuzzle.Play();

        GameObject bullet = BulletPoolManager.Instance.GetBullet(rocket);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

            if (bullet == null)
            {
                Debug.Log("No bullet");
                return;
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.right * rocketData.bulletSpeed;

        StartCoroutine(Recoil(rocketData.recoilDistance, rocketData.recoilDuration, firePoint));
    }

    
   
}
