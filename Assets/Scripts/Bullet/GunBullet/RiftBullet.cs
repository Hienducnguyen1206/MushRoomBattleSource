using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RiftBullet : Bullet
{
    public GunData riftData;
    public BulletType riftBullet;
   

    void OnCollisionEnter2D(Collision2D other)
    {
        OnHit(other,riftData,riftBullet);
    }

    public void FixedUpdate()
    {
        OnMiss(riftBullet);
    }



}
