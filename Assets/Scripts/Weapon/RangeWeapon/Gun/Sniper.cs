
using System.Collections;
using UnityEngine;

public class Sniper : Gun
{
    public GunData sniperData;
    public BulletType sniperBullet;
    public LayerMask enemyLayer;
    public Transform firePoint;
 
    Vector3 baseScale;
 

    [SerializeField] private float recoilDistance ;
    [SerializeField] private float recoilDuration ;

    public ParticleSystem SniperMuzzleFlash;

    new void Start()
    {
        StartCoroutine(AttackAfterDuration(sniperData.attackPerSecond));
       
        baseScale = transform.localScale;
        base.Start();
    }

 

    void Update()
    {
        FindNearestEnemyandRotation(playerTransform, sniperData.attackRange + sniperData.attackRange *  PlayerCurrentStats.Instance.currentAttackRange/100, enemyLayer);
        
    }



    public override void Attack()
    {   
        Attacked = true;
        SniperMuzzleFlash.transform.rotation = transform.rotation;
        SniperMuzzleFlash.Play();
        GameObject bullet = BulletPoolManager.Instance.GetBullet(sniperBullet);
        bullet.transform.rotation = firePoint.rotation; 
        bullet.transform.position = firePoint.position;
        if (bullet == null)
        {
            Debug.Log("No bullet");
            return;
        }

        // Thiết lập vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bullet.transform.right * sniperData.bulletSpeed;

        StartCoroutine(Recoil(sniperData.recoilDistance, sniperData.recoilDuration, firePoint));
    }

   
  
}
