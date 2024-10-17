
using UnityEngine;

public class WaterBullet : Bullet
{
    public MagicStaffData waterStaffData;
    public BulletType waterBullet;
    public EffectType waterHitEffect;
    GameObject WaterBulletHit;


    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);
        WaterBulletHit = EffectPoolManager.Instance.GetEffect(waterHitEffect);
        WaterBulletHit.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + other.transform.localScale.y, other.transform.position.z);
        WaterBulletHit.transform.rotation = transform.rotation;
        WaterBulletHit.transform.localScale = new Vector3(Mathf.Abs(other.transform.localScale.x * 0.25f), other.transform.localScale.y * 0.25f, other.transform.localScale.z * 0.4f);
        
        OnHit(other,waterStaffData,waterBullet);
        Invoke("DisablePaticalSystem", 0.5f);
    }

    public void FixedUpdate()
    {
        OnMiss(waterBullet);
    }
    void DisablePaticalSystem()
    {
        EffectPoolManager.Instance.ReturnEffectToPool(WaterBulletHit, waterHitEffect);

    }



}
