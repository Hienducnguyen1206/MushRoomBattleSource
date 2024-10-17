
using UnityEngine;

public class MonsterMine : MonoBehaviour
{
    [SerializeField] EffectType effectType;
    [SerializeField] MonsterData monsterData;
    Vector2 contactPoint = Vector2.zero;
    [SerializeField] BulletType bulletType;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {

            IHealthPoint hp = collider.gameObject.GetComponent<IHealthPoint>();
           
            contactPoint = collider.ClosestPoint(transform.position);
            if (hp != null)
            {
                hp.TakeDamage(monsterData.AttackDamage, false, DamageType.magic, contactPoint);
                GameObject effectToSpawn = EffectPoolManager.Instance.GetEffect(effectType);
                effectToSpawn.transform.position = gameObject.transform.position;
                effectToSpawn.transform.rotation = Quaternion.identity;
                BulletPoolManager.Instance.ReturnBulletToPool(gameObject, bulletType);
            }


        }
    }

    public void ReturnToPoolWhenWaveEnd()
    {
        BulletPoolManager.Instance.ReturnBulletToPool(gameObject, bulletType);
    }

    private void OnEnable()
    {
        GameController.KillAllMonster += ReturnToPoolWhenWaveEnd;
    }

    private void OnDisable()
    {
        GameController.KillAllMonster -= ReturnToPoolWhenWaveEnd;
    }

}
