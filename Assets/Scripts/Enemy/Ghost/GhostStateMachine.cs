using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class GhostStateMachine : StateMachine
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

    public GhostFlyState flyState;
    public GhostRangeAttackState rangeAttackState;
    public GhostIdleState idleState;

    public Transform PlayerPosition;
    public MonsterData monsterData;

    public BulletType monsterBullet;
    public Transform firePoint;

    public bool isDead;
    private PolygonCollider2D myCollider;


    private MonsterHealth monsterHealth;

    private void Awake()
    {
        flyState = new GhostFlyState(this);
        rangeAttackState = new GhostRangeAttackState(this);
        idleState = new GhostIdleState(this);
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

        skeletonAnimation.AnimationState.Event += rangeAttackState.HandleAnimationEvent;

    }






    void OnDisable()
    {
        monsterHealth.MonsterIsDead -= Dead;
        skeletonAnimation.AnimationState.Event -= rangeAttackState.HandleAnimationEvent;
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
        return flyState;
    }




    public void Flip()
    {
        if (!isDead)
        {
            Vector3 scale = gameObject.transform.localScale;
            
            if (PlayerPosition.position.x > this.transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
                gameObject.transform.localScale = scale;
            }
            else if (PlayerPosition.position.x < this.transform.position.x)
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