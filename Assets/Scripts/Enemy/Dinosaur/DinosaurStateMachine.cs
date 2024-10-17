using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class DinosaurStateMachine : StateMachine
{
    #region Animation
    [Header("Animation")]

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset walk;
    public AnimationReferenceAsset dead;
    #endregion
    public Transform pivotPoint;
    public float pivotRadius;

    public MonsterType monsterType;

    public DinosaurRunState runState;

    public DinosaurAttackState attackState;
    public DinosaurIdleState idleState;

    public Transform PlayerPosition;
    public MonsterData monsterData;

 

    public bool isDead;
    private PolygonCollider2D myCollider;


    private MonsterHealth monsterHealth;

    private void Awake()
    {
        runState = new DinosaurRunState(this);
        idleState = new DinosaurIdleState(this);
        attackState = new DinosaurAttackState(this);
        myCollider = GetComponent<PolygonCollider2D>();

    }

    private void OnEnable()
    {
        PlayerPosition = PlayerMovement.Instance.transform;

        isDead = false;
        myCollider.enabled = true;
        base.Start();
        monsterHealth = GetComponent<MonsterHealth>();
        monsterHealth.MonsterIsDead += Dead;

        skeletonAnimation.AnimationState.Event += attackState.HandleAnimationEvent;

    }






    void OnDisable()
    {
        monsterHealth.MonsterIsDead -= Dead;
        skeletonAnimation.AnimationState.Event -= attackState.HandleAnimationEvent;
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
        return runState;
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
            else if(rb.velocity.x < 0)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pivotPoint.transform.position, pivotRadius);

    }
}