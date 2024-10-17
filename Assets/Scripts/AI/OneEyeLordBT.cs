using BehaviorTree;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;




public class OneEyeLordBT : MonoBehaviour
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
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] MonsterType monsterType;
    [SerializeField] BulletType bulletType1;
    [SerializeField] BulletType bulletType2;
    [SerializeField] EffectType effectType1;
    [SerializeField] EffectType effectType2;


    MonsterStats monsterStats;
    Rigidbody2D rb;
    Vector3 position;
    float distance;

    ParticleSystem p1;
    ParticleSystem p2;
    ParticleSystem p3;
    ParticleSystem p4;

    ParticleSystem p5;




    Vector3 DirectionToPlayer;

    float currentAnimationTimeScale;



    bool Form1RangeAttackCompleted = true;
    bool Form1RangeAttacking = false;
    bool Form1MeleeAttacking = false;
    bool Form1Teleporting = false;
    bool Form1TeleportCompleted= true;


  
    bool HealingState = false;

    bool Transformed = false;
    bool Transforming = false;

    Collider2D PlayerinAttackzone;

   

    CountDownNode Form1RangeAttackCountDown;
    CountDownNode Form1TeleportCountDown;
    CountDownNode HealingCountDown;



    private void OnEnable()
    {
        skeletonAnimation.AnimationState.Event += HandleAnimationEvent;
        skeletonAnimation.AnimationState.Complete += OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete += OnDeadAnimationComplete;
    
    }

    private void OnDisable()
    {
        skeletonAnimation.AnimationState.Event -= HandleAnimationEvent;
        skeletonAnimation.AnimationState.Complete -= OnAttackAnimationComplete;
        skeletonAnimation.AnimationState.Complete -= OnDeadAnimationComplete;

    }


    void Start()
    {
        monsterStats = GetComponent<MonsterStats>();
        monsterHealth = GetComponent<MonsterHealth>();
        rb = GetComponent<Rigidbody2D>();

        p1 = gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        p2 = gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();

        p3 = gameObject.transform.GetChild(7).gameObject.GetComponent<ParticleSystem>();
        p4 = gameObject.transform.GetChild(8).gameObject.GetComponent<ParticleSystem>();

        p5 = gameObject.transform.GetChild(11).gameObject.GetComponent<ParticleSystem>();
        #region DefineBT

        tree = new BT();
        tree.rootNode = new Sequence();





        Selector AttackSelector = new Selector();

        Selector Form1AttackSelector = new Selector();
        Selector Form1MeleeAttackSelector = new Selector();
        Selector Form2MeleeAttackSelector = new Selector();

        Selector Form2AttackSelector = new Selector();

   


        Sequence Form1RangedAttackSequence= new Sequence();
        Sequence Form1TeleportSequence= new Sequence();
        Sequence Form1MeleeAttackSequence= new Sequence();

        Sequence Form2AttackSequence = new Sequence();
        Sequence Form2RangedAttackSequence= new Sequence();
        Sequence Form2TeleportSequence = new Sequence();
        Sequence Form2HealingSequence = new Sequence();


        Inverter TransformInverter = new Inverter();

        Form1RangeAttackCountDown = new CountDownNode(7f);
        Form1TeleportCountDown = new CountDownNode(8f);
        HealingCountDown = new CountDownNode(15f);





        // Condition Node
        Leaf CheckMonsterAliveLeaf = new Leaf(CheckMonsterAlive);
        Leaf CheckDistanceToRangeAttackLeaf = new Leaf(CheckDistanceToRangeAttack);
        Leaf CheckDistanceToTeleportLeaf = new Leaf(CheckDistanceToTeleport);
        Leaf CheckDistanceToMeleeAttackLeaf = new Leaf(CheckDistanceToMeleeAtack);
        Leaf CheckMonsterTransformedLeaf = new Leaf(CheckMonsterTransformed);
        Leaf CheckMonsterHpToTransformLeaf = new Leaf(CheckMonserHpToTransform);

        

        // Action Node
        //Form1
        Leaf ChaseLeaf = new Leaf(Chase);
        Leaf Form1RangeAttackLeaf = new Leaf(Form1RangeAttack);
        Leaf Form2RangeAttackLeaf = new Leaf(Form2RangeAttack);
        Leaf Form1RangeAttackResetLeaf = new Leaf(Form1RangeAttackReset);
        Leaf Form1TeleportLeaf = new Leaf(Form1Teleport);
        Leaf Form2TeleportLeaf = new Leaf(Form2Teleport);
        Leaf Form1TeleportResetLeaf = new Leaf(Form1TeleportReset);
        Leaf Form1MeleeAttackLeaf = new Leaf(Form1MeleeAttack);
        Leaf TransformToForm2Leaf = new Leaf(TransformToForm2);
        Leaf HealingLeaf = new Leaf(Healing);
        Leaf HealingCountDownResetLeaf = new Leaf(ResetHealingCountDown);


        TransformInverter.setChild(TransformToForm2Leaf);

        Form1RangedAttackSequence.AddChild(CheckDistanceToRangeAttackLeaf);
        Form1RangedAttackSequence.AddChild(Form1RangeAttackCountDown);
        Form1RangedAttackSequence.AddChild(Form1RangeAttackLeaf);
        Form1RangedAttackSequence.AddChild(Form1RangeAttackResetLeaf);


        Form2RangedAttackSequence.AddChild(CheckDistanceToRangeAttackLeaf);
        Form2RangedAttackSequence.AddChild(Form1RangeAttackCountDown);
        Form2RangedAttackSequence.AddChild(Form2RangeAttackLeaf);
        Form2RangedAttackSequence.AddChild(Form1RangeAttackResetLeaf);

        Form1TeleportSequence.AddChild(CheckDistanceToTeleportLeaf);
        Form1TeleportSequence.AddChild(Form1TeleportCountDown);
        Form1TeleportSequence.AddChild(Form1TeleportLeaf);
        Form1TeleportSequence.AddChild(Form1TeleportResetLeaf);

        Form2TeleportSequence.AddChild(CheckDistanceToTeleportLeaf);
        Form2TeleportSequence.AddChild(Form1TeleportCountDown);
        Form2TeleportSequence.AddChild(Form2TeleportLeaf);
        Form2TeleportSequence.AddChild(Form1TeleportResetLeaf);


        Form1MeleeAttackSequence.AddChild(CheckDistanceToMeleeAttackLeaf);
        Form1MeleeAttackSequence.AddChild(Form1MeleeAttackLeaf);

        Form1AttackSelector.AddChild(Form1RangedAttackSequence);
        Form1AttackSelector.AddChild(Form1MeleeAttackSelector);

        Form1MeleeAttackSelector.AddChild(Form1TeleportSequence);
        Form1MeleeAttackSelector.AddChild(Form1MeleeAttackSequence);

        Form2AttackSelector.AddChild(TransformInverter);
        Form2AttackSelector.AddChild(Form2HealingSequence);
        Form2AttackSelector.AddChild(Form2RangedAttackSequence);
       
        Form2AttackSelector.AddChild(Form2MeleeAttackSelector);


        Form2MeleeAttackSelector.AddChild(Form2TeleportSequence);
        Form2MeleeAttackSelector.AddChild(Form1MeleeAttackSequence);

        Form2HealingSequence.AddChild(HealingCountDown);
        Form2HealingSequence.AddChild(HealingLeaf);
        Form2HealingSequence.AddChild(HealingCountDownResetLeaf);

        Form2AttackSequence.AddChild(CheckMonsterHpToTransformLeaf);
        Form2AttackSequence.AddChild(Form2AttackSelector);

        

        AttackSelector.AddChild(Form2AttackSequence);
        AttackSelector.AddChild(CheckMonsterTransformedLeaf);
        AttackSelector.AddChild(Form1AttackSelector);



        tree.rootNode.AddChild(CheckMonsterAliveLeaf);
        tree.rootNode.AddChild(ChaseLeaf);
        tree.rootNode.AddChild(AttackSelector);


        #endregion
    }


    void Update()
    {
        playerPosition = PlayerMovement.Instance.transform.position;
        distance = Vector3.Distance(playerPosition, transform.position);
        tree.Tick();
    }

    private NodeState CheckMonsterAlive()
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
    private NodeState Chase()
    {
       
        if (distance < monsterData.AttackRange * 3f )
        {
          
            return NodeState.SUCCESS;
        }
        else
        {
            SetAnimation(walk, 1f);
            DirectionToPlayer = (playerPosition - transform.position).normalized;
            rb.velocity = DirectionToPlayer * monsterData.MoveSpeed;
            Flip();
            return NodeState.RUNNING;
        }
    }
    private NodeState Form1MeleeAttack()
    {             
        SetAnimation(attack, 1f);
        Form1MeleeAttacking = true;
        return NodeState.SUCCESS;
    }
    private NodeState Form1RangeAttack()
    {
       
        if (!Form1RangeAttacking && !Form1Teleporting && !Transforming)
        {
            rb.velocity = Vector2.zero;
            SetAnimation(idle, 0.9f);
            Form1RangeAttackCompleted = false;
            StartCoroutine(Form1RangeAttackCorroutine(3, 0.25f));
            Form1RangeAttacking = true;
        }

        if (Form1RangeAttackCompleted)
        {   Form1RangeAttacking = false;
            return NodeState.SUCCESS ;
        }
        else
        {   
            return NodeState.RUNNING ;
        }
    }
    private NodeState Form2RangeAttack()
    {

        if (!Form1RangeAttacking && !Form1Teleporting && !Transforming)
        {
            rb.velocity = Vector2.zero;
            SetAnimation(idle, 0.9f);
            Form1RangeAttackCompleted = false;
            StartCoroutine(Form2RangeAttackCorroutine(5, 0.25f));
            Form1RangeAttacking = true;
        }

        if (Form1RangeAttackCompleted)
        {
            Form1RangeAttacking = false;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
    private NodeState Form1Teleport() {

        if (!Form1RangeAttacking && !Form1Teleporting &&! Transforming  )
        {   
            Form1Teleporting = true;
            Form1TeleportCompleted = false;
            rb.velocity = Vector2.zero;
            SetAnimation(idle, 0.75f);
            StartCoroutine(Form1Teleport(1.25f));
        }
        Flip();    
        return NodeState.SUCCESS;
    }
    private NodeState Form2Teleport()
    {

        if (!Form1RangeAttacking && !Form1Teleporting && !Transforming)
        {
            Form1Teleporting = true;
            Form1TeleportCompleted = false;
            rb.velocity = Vector2.zero;
            SetAnimation(idle, 0.9f);
            StartCoroutine(Form2Teleport(0.75f));

        }
        Flip();
        return NodeState.SUCCESS;
    }
    private NodeState TransformToForm2()
    {   
        if (!Transformed && !Transforming && (Form1TeleportCompleted || Form1RangeAttackCompleted) )
        {
            Transforming = true;
            SetAnimation(idle);
            rb.velocity = Vector2.zero;
            StartCoroutine(TransformToForm2(3.5f));
            return NodeState.RUNNING;
        }
        else if (Transformed)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }                               
    }
    private NodeState Healing()
    {
        if (Transformed)
        {
            
            if (!HealingState )
            {   
                HealingState = true;
                SetAnimation(idle);
                rb.velocity = Vector2.zero;
                StartCoroutine(Healing(1.5f));
                return NodeState.RUNNING;
            }else 
            {
                return NodeState.SUCCESS;
            }
          
               

         
          


        }
        else
        {
            return NodeState.RUNNING;
        }
       
    }

    // Check Condition Leaf Function
    private NodeState CheckDistanceToTeleport()
    {
        if( distance > monsterData.AttackRange * 1.4f && !Form1RangeAttacking)
        {
          
            return NodeState.SUCCESS ;
        }
        else
        {   
           
            return NodeState.FAILURE ;
        }
    }
    private NodeState CheckDistanceToRangeAttack()
    {
        if(distance > monsterData.AttackRange  || Form1RangeAttacking)
        {
           
            return NodeState.SUCCESS;
        }
        else
        {   

            return NodeState.FAILURE ;
        }

    }
    private NodeState CheckDistanceToMeleeAtack()
    {
        if (Form1RangeAttacking || Form1MeleeAttacking || HealingState)
        {
            return NodeState.FAILURE;
        }

        PlayerinAttackzone = Physics2D.OverlapArea(PointA.position, PointB.position, LayerMask.GetMask("Player"));
        if (PlayerinAttackzone)
        {   

            rb.velocity = Vector2.zero;
            
            return NodeState.SUCCESS;
        }else{
            if (!Transforming)
            {
                SetAnimation(walk, 1f);
                DirectionToPlayer = (playerPosition - transform.position).normalized;
                rb.velocity = DirectionToPlayer * monsterData.MoveSpeed;
                Flip();
                return NodeState.RUNNING;
            }
            else
            {
                SetAnimation(idle);
                rb.velocity = Vector2.zero;
                return NodeState.FAILURE;
            }
            
        }
        

    }
    private NodeState CheckMonsterTransformed()
    {
        if (Transformed)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
    private NodeState CheckMonserHpToTransform()
    {
        if ((monsterHealth.currentHp < monsterHealth.maxHp/2 && !Form1MeleeAttacking && !Form1Teleporting && !Form1MeleeAttacking ) || Transformed)
        {
            
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
    // Reset Leaf Function
    private NodeState Form1RangeAttackReset()
    {
        Form1RangeAttackCountDown.Reset();
        Form1RangeAttackCompleted = false;
        
        return NodeState.SUCCESS ;
    }
    private NodeState Form1TeleportReset()
    {
      
        Form1TeleportCountDown.Reset();
        return NodeState.SUCCESS ;
    }

    private NodeState ResetHealingCountDown()
    {
        HealingCountDown.Reset();
 
        return NodeState.SUCCESS ;
    }
    // Transformed Leaf
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
    private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "OnDamaging")
        {
            if (!Transformed)
            {

                if (!p1.isPlaying)
                {
                    p1.Play();
                }

                if (!p2.isPlaying)
                {
                    p2.Play();
                }
            }
            else
            {
                if (!p3.isPlaying)
                {
                    p3.Play();
                }

                if (!p4.isPlaying)
                {
                    p4.Play();
                }
            }
            DealDamage();
        }
    }
    private void DealDamage()
    {

        Collider2D collider = Physics2D.OverlapArea(PointA.position, PointB.position, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            var hp = collider.GetComponent<IHealthPoint>();
            if (hp != null)
            {
                Vector3 hurtPosition = collider.ClosestPoint(transform.position);
                hp.TakeDamage(monsterStats.currentAttackDamage, false, DamageType.physic, hurtPosition);

            }
        }
    } 
    private void OnAttackAnimationComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Attack")
        {
            Form1MeleeAttacking = false;
       
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.GetChild(0).transform.position,monsterData.AttackRange);
        Gizmos.DrawWireSphere(transform.GetChild(0).transform.position, monsterData.AttackRange*3f);
        Gizmos.DrawWireSphere(transform.GetChild(0).transform.position, monsterData.AttackRange*1.4f);

    }
    IEnumerator  Form1RangeAttackCorroutine(int n,float t)
    {   int count = 0;

        while(count < n)
        {
            GameObject bullet = BulletPoolManager.Instance.GetBullet(bulletType1);
            bullet.transform.position = transform.GetChild(0).position;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 direction = (playerPosition - transform.GetChild(0).position).normalized;
            rb.velocity = direction * 10f;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            count++;
            
            yield return new WaitForSeconds(t);
          
        }
      
        Form1RangeAttackCompleted = true;
    }
    IEnumerator Form2RangeAttackCorroutine(int n, float t)
    {
        int count = 0;

        while (count < n)
        {
            GameObject bullet = BulletPoolManager.Instance.GetBullet(bulletType2);
            bullet.transform.position = transform.GetChild(0).position;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 direction = (playerPosition - transform.GetChild(0).position).normalized;
            rb.velocity = direction * 10f;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+180));

            count++;

            yield return new WaitForSeconds(t);

        }

        Form1RangeAttackCompleted = true;
    }
    IEnumerator Form1Teleport(float t)
    {
        GameObject effectToSpawn = EffectPoolManager.Instance.GetEffect(effectType1);
        Vector3 currentPlayerPosition = playerPosition;
        effectToSpawn.transform.position = playerPosition;
        gameObject.transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(t);
        gameObject.transform.position = currentPlayerPosition;
        gameObject.transform.GetChild(6).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
      
        
        EffectPoolManager.Instance.ReturnEffectToPool(effectToSpawn,effectType1);
        Form1Teleporting = false;
        Form1TeleportCompleted = true;       
    }
    IEnumerator Form2Teleport(float t)
    {
        GameObject effectToSpawn = EffectPoolManager.Instance.GetEffect(effectType2);
        Vector3 currentPlayerPosition = playerPosition;
        effectToSpawn.transform.position = playerPosition;
        gameObject.transform.GetChild(10).gameObject.SetActive(true);
        yield return new WaitForSeconds(t);
        gameObject.transform.position = currentPlayerPosition;
        gameObject.transform.GetChild(10).gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);


        EffectPoolManager.Instance.ReturnEffectToPool(effectToSpawn, effectType2);
        Form1Teleporting = false;
        Form1TeleportCompleted = true;


    }
    IEnumerator TransformToForm2 (float t)
    {
       
        gameObject.transform.GetChild(9).gameObject.SetActive(true);
        yield return new WaitForSeconds(t);
        skeletonAnimation.skeleton.SetSkin("V3");
        monsterStats.currentMoveSpeed *= 1.25f;
        monsterStats.currentMoveSpeed *= 1.25f;
        gameObject.transform.GetChild(9).gameObject.SetActive(false);
        Transformed = true;
        Transforming = false;
    }

    IEnumerator Healing(float t)
    {
       

        yield return new WaitForSeconds(t);
      
        ParticleSystem.MainModule mainModule = p5.main;
        mainModule.simulationSpeed = 0.4f;

        monsterHealth.currentHp += 500;
        p5.Play();
       
        HealingState = false;
    } 
       
    

}
