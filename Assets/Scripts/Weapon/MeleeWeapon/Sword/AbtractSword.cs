using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AbstractWeapon
{
    protected bool isAttacking = false;
    protected bool isAttacked = false;
    protected bool inEnemyCollider = false;
    protected Vector3 originalPosition;
    protected Vector3 attackTargetPosition;

 
    Vector3 RandomDirection ;
    float Randomoffset;
    protected Vector3 HurtPoint ;
    public override void Attack()
   {

   }


    public override void FindNearestEnemyandRotation(Transform playerTransform, float attackRange, LayerMask enemyLayer)
    {
        if (!isAttacking)
        {
            Collider2D[] results = new Collider2D[10];
            int colliders = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, results, enemyLayer);

            List<GameObject> enemyList = new List<GameObject>();

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] != null)
                {
                    enemyList.Add(results[i].gameObject);
                }
            }

            GameObject[] enemys = enemyList.ToArray();



            enemyInRange = colliders > 0;
            if (enemyInRange)
            {
                gameObject.transform.localScale = normalScale;
            }

            // Tìm kẻ địch gần nhất 
            float minDistance = Mathf.Infinity;
            Collider2D nearestEnemyCollider = null;


            for (int i = 0; i < colliders; i++)
            {
                Collider2D collider = results[i];

                {
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemyCollider = collider;
                    }
                }
            }

            // Xoay vũ khí tới kẻ địch gần nhất 
            Vector3 direction = Vector3.zero;
            if (nearestEnemyCollider != null)
            { 
                Vector3 hurtposition = nearestEnemyCollider.gameObject.transform.GetChild(0).position;
                
                
                HurtPoint = hurtposition + RandomDirection * nearestEnemyCollider.gameObject.transform.localScale.y*Randomoffset;
                direction = HurtPoint - transform.position;
                direction.z = 0f;

                float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;

                if (playerTransform.position.x < nearestEnemyCollider.transform.position.x)
                {
                    transform.localScale = normalScale;
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = normalScale;
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                }
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                transform.rotation = baseRotation;
                if (PlayerMovement.Instance.isFliped)
                {
                    transform.localScale = new Vector3(-normalScale.x, normalScale.y, normalScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(normalScale.x, normalScale.y, normalScale.z);
                }
            }

            // Trả về hướng và biến kiểm tra có kẻ địch trong tầm hay không;
            
            
        } 
        
    }

    protected IEnumerator MoveToAttact(SwordData swordData)
    {
        if (isAttacking)
            yield break;

        isAttacking = true;
        
      
        Vector3 attackDirection = transform.right.normalized;
        attackTargetPosition = originalPosition + attackDirection * swordData.attackRange;

        float elapsedTime = 0f;

        while (elapsedTime < swordData.attackDuration)
        {
            transform.localPosition = Vector2.Lerp(originalPosition, attackTargetPosition, elapsedTime / swordData.attackDuration);
            elapsedTime += Time.deltaTime;
                 
            yield return null;
        }
        isAttacked = true;

        
        elapsedTime = 0f;
        
        while (elapsedTime < swordData.attackDuration)
        {
            transform.localPosition = Vector2.Lerp(attackTargetPosition, originalPosition, elapsedTime / swordData.attackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RandomDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized;
        Randomoffset = Random.Range(0,0.7f);
        transform.localPosition = originalPosition;
        isAttacking = false;
        
    }

}
