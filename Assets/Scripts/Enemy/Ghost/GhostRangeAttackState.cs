using Spine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class GhostRangeAttackState : BaseState
{
    private GhostStateMachine SM;
   

    public GhostRangeAttackState(GhostStateMachine stateMachine) : base("RangeAttack", stateMachine)
    {
        SM = stateMachine;
    }


    public override void Enter()
    {
        base.Enter();
        var animationState = SM.skeletonAnimation.AnimationState;
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.attack, true).TimeScale = SM.monsterData.AttackPerSecond;
        animationState.Complete += OnAttackAnimationComplete;

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);

        if (distance > SM.monsterData.AttackRange && SM.isDead == false)
        {
            SM.ChangeState(SM.idleState);
        }

    }

    public override void UpdatePhysic()
    {
        base.UpdatePhysic();

    }

    public override void Exit()
    {
        base.Exit();
        SM.skeletonAnimation.AnimationState.Complete -= OnAttackAnimationComplete;
    }

    public void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "OnDamaging")
        {
            DealDamage();

        }
    }

    private void DealDamage()
    {
        GameObject bulletToSpawn = BulletPoolManager.Instance.GetBullet(SM.monsterBullet);
        Rigidbody2D rb = bulletToSpawn.GetComponent<Rigidbody2D>();

        
        Vector3 direction = ((SM.PlayerPosition.position + new Vector3 (0,0.2f,0))  - SM.firePoint.transform.position).normalized;
     
        
        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;

      
        bulletToSpawn.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bulletToSpawn.transform.position = SM.firePoint.position;

        
        rb.velocity = direction * 3f;
    }



    private void OnAttackAnimationComplete(TrackEntry trackEntry)
    {

        SM.Flip();
    }
}
