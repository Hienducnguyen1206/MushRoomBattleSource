
using UnityEngine;

public class LaserBullet : Bullet
{
    public GunData LaserGunData;
    public BulletType laserBullet;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnHit(other,LaserGunData,laserBullet);
    }

 
    public void FixedUpdate()
    {
        OnMiss(laserBullet);
    }


}
