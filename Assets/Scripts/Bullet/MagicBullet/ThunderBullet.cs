
using UnityEngine;


public class ThunderBullet : Bullet
{
    public MagicStaffData ThunderStaffData;
    public BulletType ThunderBulletType;
    public EffectType[] lightningShock;
    EffectType lightningToSpawn;
    GameObject lightningEffect;

    void OnCollisionEnter2D(Collision2D other)
    {
        lightningToSpawn = Services.RandomObject(lightningShock);
        lightningEffect = EffectPoolManager.Instance.GetEffect(lightningToSpawn);
        lightningEffect.transform.position = new Vector3(other.transform.position.x, other.transform.position.y+0.5f+(other.transform.localScale.y / 2), other.transform.rotation.z);
        lightningEffect.transform.rotation = transform.rotation;
        lightningEffect.transform.localScale = new Vector3(other.transform.localScale.x / 2, other.transform.localScale.y / 2, other.transform.localScale.z / 2);

        OnHit(other, ThunderStaffData, ThunderBulletType);
        Invoke("DisablePaticalSystem", 0.5f);
    }

    void DisablePaticalSystem()
    {
        EffectPoolManager.Instance.ReturnEffectToPool(lightningEffect,lightningToSpawn );

    }

    public void FixedUpdate()
    {
        OnMiss(ThunderBulletType);
    }

}
