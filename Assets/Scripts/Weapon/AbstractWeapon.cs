using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    protected bool enemyInRange = false;
    public Transform playerTransform;
    protected Transform weaponTransform;
    protected Quaternion baseRotation;
    protected Vector3 normalScale;
    protected Vector3 flipXScale;
    protected Vector3 flipYScale;

    private List<GameObject> enemyList = new List<GameObject>(); 
    private Collider2D[] results = new Collider2D[15];
    int colliders;
    public bool Attacked;

    public virtual void Start()
    {   
        playerTransform = PlayerMovement.Instance.transform;
        weaponTransform = transform;
        normalScale = transform.localScale;
        flipXScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        flipYScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        baseRotation = transform.rotation;
        Attacked = true;
    }

    public virtual void FindNearestEnemyandRotation(Transform playerTransform, float attackRange, LayerMask enemyLayer)
    {
        
            enemyList.Clear();
        if (Attacked)
        {
            colliders = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, results, enemyLayer);
            for (int i = 0; i < colliders; i++)
            {
                if (results[i] != null)
                {
                    enemyList.Add(results[i].gameObject);
                }
            }
        }
            enemyInRange = colliders > 0;


            float minDistance = Mathf.Infinity;
            Collider2D nearestEnemyCollider = null;

            for (int i = 0; i < colliders; i++)
            {
                Collider2D collider = results[i];
                if (collider != null)
                {
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemyCollider = collider;
                    }
                }
            }

            // Rotate the weapon to the nearest enemy
            Vector3 direction = Vector3.zero;
            if (nearestEnemyCollider != null )
            {   
                
                RotateToNearestEnemy(direction, nearestEnemyCollider);
                Attacked = false;
            }
            else
            {   
                FlipWhenNotHaveEnemyInRange();
            }
            
        
        
    }

    public abstract void Attack();

    public virtual void RotateToNearestEnemy(Vector3 direction, Collider2D nearestEnemyCollider)
    {
        direction = nearestEnemyCollider.transform.position - transform.position;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y + nearestEnemyCollider.transform.localScale.y, direction.x) * Mathf.Rad2Deg;

        if (playerTransform.position.x < nearestEnemyCollider.transform.position.x)
        {
            transform.localScale = normalScale;

           
        }
        else
        {
            transform.localScale = flipYScale;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void FlipWhenNotHaveEnemyInRange()
    {
        transform.rotation = baseRotation;

        if (PlayerMovement.Instance.isFliped)
        {
            transform.localScale = flipXScale;
        }
        else
        {
            transform.localScale = normalScale;
        }
    }

    
    public virtual IEnumerator AttackAfterDuration(float attackPerSecond)
    {
        while (true)
        {
            if (enemyInRange)
            {
                Attack();
                
                yield return new WaitForSeconds(1/ (attackPerSecond+ attackPerSecond * PlayerCurrentStats.Instance.currentAttackSpeed/100));
            }
            else
            {
                yield return null;
            }
        }
    }
}
