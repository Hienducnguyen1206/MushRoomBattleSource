
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Rift : Gun
{
    public GunData riftData;
    public BulletType riftBullet;

    public LayerMask enemyLayer;
    public Transform firePoint;

 
   



    public List<ParticleSystem> riftMuzzleFlashs;


    public override void  Start()
    {
        StartCoroutine(AttackAfterDuration(riftData.attackPerSecond));          
        base.Start();
    }

   
    void Update()
    {
      
        FindNearestEnemyandRotation(playerTransform, riftData.attackRange + riftData.attackRange * PlayerCurrentStats.Instance.currentAttackRange/100, enemyLayer);
        
    }

    public T GetRandomElement<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new System.ArgumentException("The list is null or empty.");
        }

        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

    public override void Attack()
    {   
        Attacked = true;
        ParticleSystem  riftMuzzleFlash = GetRandomElement(riftMuzzleFlashs);
        riftMuzzleFlash.transform.rotation = transform.rotation;
        riftMuzzleFlash.Play();
       
        GameObject bullet = BulletPoolManager.Instance.GetBullet(riftBullet);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation; 
            if (bullet == null)
            {
                Debug.Log("No bullet");
                return;
            }

            // Thiết lập vận tốc cho đạn
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.right * riftData.bulletSpeed;


        StartCoroutine(Recoil(riftData.recoilDistance, riftData.recoilDuration, firePoint));
    }

 

    
}
