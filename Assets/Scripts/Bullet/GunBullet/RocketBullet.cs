using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBullet : Bullet
{
    public GunData BazookaData;
    public BulletType rocket;
    public EffectType rocketExplosionEffect;
    public float explosionRadius;
    public LayerMask enemyLayer;
    GameObject rocketExplosion;
    private Collider2D[] colliders = new Collider2D[10];



    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);
        Vector3 position = new Vector3(contactPoint.point.x, contactPoint.point.y, 0);       
        rocketExplosion = EffectPoolManager.Instance.GetEffect(rocketExplosionEffect);
        rocketExplosion.transform.position = position;
        rocketExplosion.transform.rotation = Quaternion.identity;

        int numColliders = Physics2D.OverlapCircleNonAlloc(position, explosionRadius, colliders, enemyLayer);

        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i] != null)
            {
                OnHit(colliders[i],BazookaData,rocket);
            }
        }       
        Invoke("DisablePaticalSystem", 0.5f);
    }

    private void FixedUpdate()
    {
        OnMiss(rocket);
    }

    void DisablePaticalSystem()
    {
        EffectPoolManager.Instance.ReturnEffectToPool(rocketExplosion, rocketExplosionEffect);
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
