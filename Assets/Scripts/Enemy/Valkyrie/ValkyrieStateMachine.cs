using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;


public class ValkyrieStateMachine : StateMachine
{
    #region Animation
    [Header("Animation")]

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset dead;
    #endregion



    public MonsterType monsterType;

    public ValkyrieFlyState flyState;
    public ValkyrieAttackState attackState;
    public ValkyrieRushState rushState;
    public ValkyrieIdleState idleState;

    public Transform PlayerPosition;
    public MonsterData monsterData;

    public PolygonCollider2D playzone;
    public float MaxX;
    public float MaxY;


    public bool isDead;
    private PolygonCollider2D myCollider;
    private MonsterHealth monsterHealth;

    public bool isPatroling = true;

    private void Awake()
    {
        
        myCollider = GetComponent<PolygonCollider2D>();
        rushState = new ValkyrieRushState(this);
        attackState = new ValkyrieAttackState(this);
        flyState = new ValkyrieFlyState(this);
        idleState = new ValkyrieIdleState(this);

        
    }

    private void OnEnable()
    {
        // PlayerPosition = PlayerMovement.Instance.transform;

        isDead = false;
        myCollider.enabled = true;
        base.Start();
        monsterHealth = GetComponent<MonsterHealth>();
        monsterHealth.MonsterIsDead += Dead;

       // skeletonAnimation.AnimationState.Event += attackState.HandleAnimationEvent;

    }

    private new void Start()
    {
        PlayerPosition = PlayerMovement.Instance.transform;
    }




    void OnDisable()
    {
        monsterHealth.MonsterIsDead -= Dead;
        //skeletonAnimation.AnimationState.Event -= attackState.HandleAnimationEvent;
    }

    new void Update()
    {
        base.Update();
    }


    public void ChangeState(BaseState newState)
    {
        changeState(newState);
    }



    protected override BaseState GetInitialState()
    {
        return idleState;
    }




    public void Flip()
    {
        if (!isDead)
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
    }

    public void Dead()
    {
        isDead = true;
        myCollider.enabled = false;
        StartCoroutine(DeadAnimation());
    }

    IEnumerator DeadAnimation()
    {
        TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, dead, false);

        while (!trackEntry.IsComplete)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        MonsterPoolManager.Instance.ReturnMonsterToPool(gameObject, monsterType);

    }

  
}