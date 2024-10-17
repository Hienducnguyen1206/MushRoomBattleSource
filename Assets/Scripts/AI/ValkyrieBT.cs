using BehaviorTree;
using Spine;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;



public class ValkyrieBT : MonoBehaviour
{
 

    #region Animation
    [Header("Animation")]

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset dead;
    private AnimationReferenceAsset currentAnimationClip;
    
    #endregion

    BT tree;

    [SerializeField] Vector3 playerPosition;
    [SerializeField] MonsterData monsterData;
    [SerializeField] MonsterHealth monsterHealth;
    [SerializeField] float MaxX;
    [SerializeField] float MaxY;
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] PolygonCollider2D playzone;
    [SerializeField] MonsterType monsterType;



    Rigidbody2D rb;
    Vector3 position;
    float distance;
   
    float x, y;
    bool isPatrolling = false;
    bool isChasing = false; 
    TimeLimit idleTimeLimit;
    Vector3 DirectionToPlayer;
    float detectRange;
    float currentAnimationTimeScale;

    bool idleStatus = true;
    bool attacking = false;

    Collider2D PlayerinAttackzone;
    private void OnEnable()
    {
        skeletonAnimation.AnimationState.Event += HandleAnimationEvent;
        skeletonAnimation.AnimationState.Complete +=OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete += OnDeadAnimationComplete;
        skeletonAnimation.AnimationState.Complete += OnIdleAnimationComplete;
    }

    private void OnDisable()
    {
        skeletonAnimation.AnimationState.Event -= HandleAnimationEvent;
        skeletonAnimation.AnimationState.Complete -= OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete -= OnDeadAnimationComplete;
        skeletonAnimation.AnimationState.Complete -= OnIdleAnimationComplete;
    }


    void Start()
    {   
        detectRange = monsterData.AttackRange * 4f;
        monsterHealth = GetComponent<MonsterHealth>();
        rb = GetComponent<Rigidbody2D>();
        playzone = GameController.Instance.GetComponent<PolygonCollider2D>();
        
        #region DefineBT
        tree = new BT();
        tree.rootNode = new Sequence();
        Leaf CheckMonsterAlive = new Leaf(isAlive);
        Leaf patrol = new Leaf(Patrol);
        Leaf chase = new Leaf(Chase);
        Leaf attack = new Leaf(Attack);
        Leaf resetTime = new Leaf(ResetTimeLimit);
        Leaf dectectPlayer = new Leaf(DetechPlayer);
        Leaf idleStatus = new Leaf(idleStatusLeaf);
        Selector idleSelector = new Selector();
        Sequence idleSequence = new Sequence(); 
        

        
        idleTimeLimit  = new TimeLimit();
        
       
        Leaf idleState = new Leaf(Idle);

        /*
        idleTimeLimit.SetRunTime(2);
        idleTimeLimit.setChild(idleState);

        idleSelector.AddChild(dectectPlayer);
        idleSelector.AddChild(idleSequence);

        idleSequence.AddChild(resetTime);
        idleSequence.AddChild(idleTimeLimit);

        attackSelector.AddChild(attack);
        attackSelector.AddChild(idleSequence);

        tree.rootNode.AddChild(monsterisAlive); 
        tree.rootNode.AddChild(idleSelector);      
        tree.rootNode.AddChild(patrol);
        tree.rootNode.AddChild(chase);
        tree.rootNode.AddChild(attackSelector);
        */

        idleSelector.AddChild(idleStatus);
        idleSelector.AddChild(idleState);

        tree.rootNode.AddChild(CheckMonsterAlive);
        tree.rootNode.AddChild(idleSelector);
        tree.rootNode.AddChild(patrol);
        tree.rootNode.AddChild(chase);
        tree.rootNode.AddChild(attack);
        #endregion
    }

   
    void Update()
    {
        playerPosition = PlayerMovement.Instance.transform.position;
        distance = Vector3.Distance(playerPosition, transform.position);
        tree.Tick();
    }



    private NodeState Patrol()
    {
        
        if (distance > detectRange)
        {
            GetRandomPointAndMove();
            SetAnimation(walk, 1f);
            return NodeState.RUNNING;

        } else
        {            
            return NodeState.SUCCESS;
        }      
    }
    private NodeState Idle()
    {
        SetAnimation(idle, 1f);
        rb.velocity = Vector2.zero;
        if (idleStatus && distance < 2f)
        {
          
            return NodeState.RUNNING;

        }

        idleStatus = false;
        return NodeState.SUCCESS;
                       
    }
    private NodeState Attack()
    {
        
           attacking = true;
           rb.velocity = Vector2.zero;
           SetAnimation(attack, monsterData.AttackPerSecond);
           return NodeState.SUCCESS;
        
       
            
       
    }
    private NodeState Chase()
    {
       
         PlayerinAttackzone = Physics2D.OverlapArea(PointA.position, PointB.position, LayerMask.GetMask("Player"));
        if ( PlayerinAttackzone ) 
        {  
            return NodeState.SUCCESS;
        }
        else if(distance < detectRange )
        {  
            isChasing = true;          
            SetAnimation(walk, 2f);
            DirectionToPlayer = (playerPosition - transform.position).normalized;
            rb.velocity = DirectionToPlayer * monsterData.MoveSpeed * 1.5f;
            Flip();
          //  distance = Vector3.Distance(playerPosition, transform.position);
            return NodeState.RUNNING;
        }
        else
        {

            return NodeState.FAILURE;
        }
       
           
                   
           
        
        
    }
    private NodeState ResetTimeLimit()
    {
        if(isChasing && distance > detectRange)
        {   isChasing=false;
            DirectionToPlayer = (playerPosition - transform.position).normalized;
            rb.velocity = DirectionToPlayer * monsterData.MoveSpeed * 0.05f;
            Flip();
            idleTimeLimit.Reset(); 
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
    private NodeState isAlive()
    {    
        if(!monsterHealth.isDead)
        {
            return NodeState.SUCCESS;
        }
        else
        {   
            rb.velocity = Vector3 .zero;
            SetAnimation(dead, 1f);
            return NodeState.FAILURE;
        }
       
    }
    private NodeState DetechPlayer()
    {
        if (distance < detectRange)
        {
            return NodeState.SUCCESS;
        }else
        {
            return NodeState.FAILURE;
        }
    }



    private NodeState idleStatusLeaf()
    {
       
        if(attacking == true && !PlayerinAttackzone)
        {
         
            idleStatus = true;
            attacking = false;
        }

        if(!idleStatus)
        {
            return NodeState.SUCCESS ;
        }
        else
        {
            
            return NodeState.FAILURE;
        }
    }









    private void Flip()
    {
        
            Vector3 scale = gameObject.transform.localScale;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.velocity.x > 0 )
            {
                scale.x = Mathf.Abs(scale.x);
                gameObject.transform.localScale = scale;
            }
            else if (rb.velocity.x < 0 )
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
        if(currentAnimationClip != newAnimationClip || currentAnimationTimeScale != timeScale)
        {   
            currentAnimationClip = newAnimationClip;
            currentAnimationTimeScale = timeScale;
            skeletonAnimation.AnimationState.SetAnimation(0, newAnimationClip, true).TimeScale = currentAnimationTimeScale;
        }      
    }
    private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "OnDamaging")
        {
            DealDamage();

        }
    }
    private void DealDamage()
    {

        Collider2D collider = Physics2D.OverlapArea(PointA.position,PointB.position , LayerMask.GetMask("Player"));
        if (collider != null)
        {
            var hp = collider.GetComponent<IHealthPoint>();
            if (hp != null)
            {
                Vector3 hurtPosition = collider.ClosestPoint(transform.position);
                hp.TakeDamage(monsterData.AttackDamage, false, DamageType.physic, hurtPosition);

            }
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
        }
    }
    private void OnAttackAnimationComplete(TrackEntry trackEntry)
    {
       if(trackEntry.Animation.Name == "Attack")
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
    private void OnIdleAnimationComplete(TrackEntry trackEntry)
    {
        idleStatus = false;
       
    }
}
