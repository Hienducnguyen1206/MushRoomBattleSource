
using System.Collections;
using UnityEngine;

public class ShotGun : Gun
{ 
    public GunData shotgunData;
    public BulletType shotgunBullet;
    public LayerMask enemyLayer;
    public Transform firePoint;
    
   
    Vector3 baseScale;
  



    public ParticleSystem ShotgunMuzzleFlash;


    new void Start()
    {
        base.Start();
        baseScale = transform.localScale;
        StartCoroutine(AttackAfterDuration(shotgunData.attackPerSecond));
      
       
       
    }

    private void FixedUpdate()
    {
        FindNearestEnemyandRotation(playerTransform, shotgunData.attackRange + shotgunData.attackRange* PlayerCurrentStats.Instance.currentAttackRange / 100, enemyLayer);
            
    }



    



    public override void Attack()
    {
        Attacked = true;
        ShotgunMuzzleFlash.transform.rotation = transform.rotation;
        ShotgunMuzzleFlash.Play();
        
        for (int i = 0; i < 5; i++)
        {
            float zAngle = Random.Range(-10f, 10f);
            Quaternion bulletAngle = Quaternion.Euler(0, 0, zAngle);
            Quaternion finalRotation = firePoint.rotation * bulletAngle;
            GameObject bullet = BulletPoolManager.Instance.GetBullet(shotgunBullet);
            bullet.transform.rotation = finalRotation;
            bullet.transform.position = firePoint.position;
            
            if (bullet == null)
            {
                Debug.Log("No bullet");
                return;
            }

            // Thiết lập vận tốc cho đạn
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.right * shotgunData.bulletSpeed;
            
        }
        // Bắt đầu hiệu ứng giật
        StartCoroutine(Recoil(shotgunData.recoilDistance, shotgunData.recoilDuration, firePoint));
        
    }

    

   
    
}
