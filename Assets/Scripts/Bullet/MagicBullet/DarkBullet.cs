
using UnityEngine;

public class DarkBullet : Bullet
{
    public MagicStaffData darkStaffData;
    public BulletType darkBullet;
    public EffectType darkHitEffect;
 
    GameObject DarkBulletHit;


    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);
        DarkBulletHit = EffectPoolManager.Instance.GetEffect(darkHitEffect);
        DarkBulletHit.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + other.transform.localScale.y, other.transform.position.z);
        DarkBulletHit.transform.rotation = transform.rotation;
        DarkBulletHit.transform.localScale = new Vector3(Mathf.Abs(other.transform.localScale.x * 0.38f), other.transform.localScale.y * 0.38f, other.transform.localScale.z * 0.4f);
       

       OnHit(other,darkStaffData,darkBullet);

        Invoke("DisablePaticalSystem", 0.5f);
    }

    public void FixedUpdate()
    {
       OnMiss(darkBullet);
    }
    void DisablePaticalSystem()
    {
        EffectPoolManager.Instance.ReturnEffectToPool(DarkBulletHit, darkHitEffect);

    }



}
