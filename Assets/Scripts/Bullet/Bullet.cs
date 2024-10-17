using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected bool critical;
    protected Vector3 hurtPosition;
   

    private void OnEnable()
    {
        critical = false;
    }

    public void OnHit<H, W>(H hit, W weaponData, BulletType bulletType)
    {
        IHealthPoint hp = null;
        IBloodEffect ef = null;
        Vector2 contactPoint = Vector2.zero;

        if (hit is Collision2D collision)
        {
            hp = collision.gameObject.GetComponent<IHealthPoint>();
            ef = collision.gameObject.GetComponent<IBloodEffect>();
            contactPoint = collision.GetContact(0).point;
        }
        else if (hit is Collider2D collider)
        {
            hp = collider.gameObject.GetComponent<IHealthPoint>();
            ef = collider.gameObject.GetComponent<IBloodEffect>();
            contactPoint = collider.ClosestPoint(transform.position);
        }
        else
        {
            Debug.LogError("Hit isn't Collider2D or Collision2D");
            return; // Early exit
        }

        hurtPosition = new Vector3(contactPoint.x, contactPoint.y, 0);

        if (weaponData is GunData gunData)
        {
            critical = Services.Chance(gunData.critChance + PlayerCurrentStats.Instance.currentPlayerCritChance);
            ef?.BloodEffect(contactPoint);

            int damage = critical
                ? Mathf.FloorToInt((gunData.attackDamage + PlayerCurrentStats.Instance.currentRangeDamage) * PlayerCurrentStats.Instance.currentCritialDamagemultiple)
                : Mathf.FloorToInt((gunData.attackDamage + PlayerCurrentStats.Instance.currentRangeDamage));

            if (hp != null)
            {
                hp.TakeDamage(damage, critical, DamageType.physic, hurtPosition);
            }
            else
            {
                Debug.LogError("No IHealthPoint component found on the hit object.");
            }

            BulletPoolManager.Instance.ReturnBulletToPool(gameObject, bulletType);
        }
        else if (weaponData is MagicStaffData magicStaffData)
        {
            critical = Services.Chance(magicStaffData.critChance + PlayerCurrentStats.Instance.currentPlayerCritChance);

            int damage = critical
                ? Mathf.FloorToInt((magicStaffData.attackDamage + PlayerCurrentStats.Instance.currentElementalDamage) * PlayerCurrentStats.Instance.currentCritialDamagemultiple)
                : Mathf.FloorToInt((magicStaffData.attackDamage + PlayerCurrentStats.Instance.currentElementalDamage));

            if (hp != null)
            {
                hp.TakeDamage(damage, critical, DamageType.magic, hurtPosition);
            }
            else
            {
                Debug.LogError("No IHealthPoint component found on the hit object.");
            }

            BulletPoolManager.Instance.ReturnBulletToPool(gameObject, bulletType);
        }
        else
        {
            Debug.LogError("No weapon data provided.");
        }
    }

    public virtual void OnMiss(BulletType bulletType)
    {
        if (transform.position.sqrMagnitude > 500f)
        {
            BulletPoolManager.Instance.ReturnBulletToPool(gameObject, bulletType);
        }
    }
}
