
using UnityEngine;

public class SniperBullet : Bullet
{
    public GunData sniperData;
    public BulletType sniperBullet;
   
   

    void OnCollisionEnter2D(Collision2D other)
    {
       OnHit(other,sniperData,sniperBullet);
    }

    public void FixedUpdate()
    {
        OnMiss(sniperBullet);
    }



}
