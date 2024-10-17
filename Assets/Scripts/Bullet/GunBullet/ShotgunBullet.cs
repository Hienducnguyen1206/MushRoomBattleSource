using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    public GunData shotgunData;
    public BulletType shotgunBullet;

    void OnCollisionEnter2D(Collision2D other)
    {
        OnHit(other,shotgunData,shotgunBullet);      
    }
    public void FixedUpdate()
    {
        OnMiss(shotgunBullet);
    }



}
