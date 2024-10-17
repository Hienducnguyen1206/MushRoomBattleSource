using BehaviorTree;
using Spine;
using Spine.Unity;
using UnityEngine;



public class BookBT : MonoBehaviour
{


    #region Animation
    [Header("Animation")]

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset dead;
    private AnimationReferenceAsset currentAnimationClip;



    public float bulletSpeed = 3.5f;

    #endregion

    BT tree;

    [SerializeField] Vector3 playerPosition;
    [SerializeField] MonsterData monsterData;
    [SerializeField] MonsterHealth monsterHealth;
    [SerializeField] float MaxX;
    [SerializeField] float MaxY;
    [SerializeField] PolygonCollider2D playzone;
    [SerializeField] MonsterType monsterType;
    [SerializeField] BulletType bulletType;



    Rigidbody2D rb;
    Vector3 position;
  

    float x, y;
    bool isPatrolling = false;

    float currentAnimationTimeScale;









    TimeRepeater attackRepeater;

    private void OnEnable()
    {

        skeletonAnimation.AnimationState.Complete += OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete += OnDeadAnimationComplete;

    }

    private void OnDisable()
    {

        skeletonAnimation.AnimationState.Complete -= OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete -= OnDeadAnimationComplete;

    }


    void Start()
    {

        monsterHealth = GetComponent<MonsterHealth>();
        rb = GetComponent<Rigidbody2D>();
        playzone = GameController.Instance.GetComponent<PolygonCollider2D>();


        #region DefineBT
        tree = new BT();
        tree.rootNode = new Sequence();
        Leaf CheckMonsterAlive = new Leaf(isAlive);
        Leaf patrol = new Leaf(Patrol);
        Leaf attack = new Leaf(Attack);
        attackRepeater = new TimeRepeater(3,0.75f);
        attackRepeater.childNode = attack;













        tree.rootNode.AddChild(CheckMonsterAlive);
        tree.rootNode.AddChild(patrol);
        tree.rootNode.AddChild(attackRepeater);
        #endregion
    }


    void Update()
    {
        playerPosition = PlayerMovement.Instance.transform.position;
      
        tree.Tick();
    }



    private NodeState Patrol()
    {


        GetRandomPointAndMove();
        SetAnimation(walk, 1f);
        return NodeState.SUCCESS;


    }

    private NodeState Attack()
    {

        ShootBulletsInAllDirections(8);

        return NodeState.SUCCESS;
    }

    private NodeState isAlive()
    {
        if (!monsterHealth.isDead)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            rb.velocity = Vector3.zero;
            SetAnimation(dead, 1f);
            return NodeState.FAILURE;
        }

    }
    private void Flip()
    {

        Vector3 scale = gameObject.transform.localScale;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
            gameObject.transform.localScale = scale;
        }
        else if (rb.velocity.x < 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            gameObject.transform.localScale = scale;
        }

    }
    private void FlipWhenAttack()
    {
        Vector3 scale = gameObject.transform.localScale;

        if (playerPosition.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
            gameObject.transform.localScale = scale;
        }
        else if (playerPosition.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            gameObject.transform.localScale = scale;
        }
    }
    private void SetAnimation(AnimationReferenceAsset newAnimationClip, float timeScale = 1f)
    {
        if (currentAnimationClip != newAnimationClip || currentAnimationTimeScale != timeScale)
        {
            currentAnimationClip = newAnimationClip;
            currentAnimationTimeScale = timeScale;
            skeletonAnimation.AnimationState.SetAnimation(0, newAnimationClip, true).TimeScale = currentAnimationTimeScale;
        }
    }


    private void GetRandomPointAndMove()
    {
        if (!isPatrolling)
        {
            while (true)
            {
                x = Random.Range(-MaxX, MaxX);
                y = Random.Range(-MaxY, MaxY);


                position = new Vector3(x, y, 0);

                if (playzone.OverlapPoint(position))
                {
                    isPatrolling = true;
                    break;
                }

            }
        }


        rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 moveDirection = (position - transform.position).normalized;
        rb.velocity = moveDirection * monsterData.MoveSpeed;
        Flip();




        if (Vector3.Distance(position, gameObject.transform.position) < 0.1f)
        {
            rb.velocity = Vector3.zero;
            isPatrolling = false;
            attackRepeater.ResetRepeater();
        }
    }






    private void OnAttackAnimationComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Attack")
        {
            FlipWhenAttack();
        }
    }
    private void OnDeadAnimationComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Dead")
        {
            MonsterPoolManager.Instance.ReturnMonsterToPool(gameObject, monsterType);
        }
    }

    void ShootBulletsInAllDirections(int numberOfBullets)
    {
        float angleStep = 360f / numberOfBullets; 
        float angle = 0f; 
        
        for (int i = 0; i < numberOfBullets; i++)
        {
           
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

          
            Vector3 bulletDirection = new Vector3(bulletDirX, bulletDirY, 0f).normalized;        
            GameObject bulletToSpawn = BulletPoolManager.Instance.GetBullet(bulletType);

            Vector3 bulletRotation = new Vector3(0, 0, angle);
            bulletToSpawn.transform.rotation = Quaternion.Euler(bulletRotation);


            bulletToSpawn.transform.position = transform.position;       
            bulletToSpawn.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

          
            angle += angleStep;
        }
    }


   
}
