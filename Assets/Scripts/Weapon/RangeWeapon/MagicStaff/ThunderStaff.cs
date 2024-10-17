
using UnityEngine;

public class ThunderStaff : MagicStaff
{
    public MagicStaffData thunderStaffData;
    public BulletType[] thunderBulletList;
    public LayerMask enemyLayer;
    public Transform firePoint;
    Vector3 baseScale;
  


    // Start is called before the first frame update
    public override void Start()
    {
        StartCoroutine(AttackAfterDuration(thunderStaffData.attackPerSecond));
        baseScale = transform.localScale;
        base.Start();
         
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemyandRotation(playerTransform, thunderStaffData.attackRange + thunderStaffData.attackRange *  PlayerCurrentStats.Instance.currentAttackRange/100, enemyLayer);
            
    }


    public override void Attack()
    {
        Attacked = true;
        BulletType thunderBulletToSpawn = Services.RandomObject(thunderBulletList);
        GameObject bullet = BulletPoolManager.Instance.GetBullet(thunderBulletToSpawn);
        bullet.transform.rotation = firePoint.rotation;
        bullet.transform.position = firePoint.position;


        if (bullet == null)
        {
            Debug.Log("No bullet");
            return;
        }

        // Thiết lập vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bullet.transform.right * thunderStaffData.magicSpeed;


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, thunderStaffData.attackRange );
    }
    private void OnDrawGizmos()
    {

        // Thiết lập màu cho đường thẳng
        Gizmos.color = Color.red;

       
    }

   
}
