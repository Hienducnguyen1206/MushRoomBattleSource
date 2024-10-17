
using UnityEngine;

public class FireBullet : Bullet
{
    public MagicStaffData fireStaffData;
    public BulletType fireBullet;
    public EffectType fireHitEffect;
    GameObject FireBulletHit;


    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);
        FireBulletHit = EffectPoolManager.Instance.GetEffect(fireHitEffect);
        FireBulletHit.transform.position = new Vector3(other.transform.position.x, other.transform.position.y+other.transform.localScale.y, other.transform.position.z);
        FireBulletHit.transform.rotation = transform.rotation;
        FireBulletHit.transform.localScale = new Vector3(Mathf.Abs(other.transform.localScale.x * 0.4f), other.transform.localScale.y * 0.4f, other.transform.localScale.z * 0.4f);


        OnHit(other, fireStaffData, fireBullet);
        Invoke("DisablePaticalSystem", 0.5f);
    }

    public void FixedUpdate()
    {
        OnMiss(fireBullet);
    }
    void DisablePaticalSystem()
    {
        EffectPoolManager.Instance.ReturnEffectToPool(FireBulletHit, fireHitEffect);

    }



}
