using Spine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class NormalAttackState : BaseState
{
    private NormalStateMachine SM;
    

    public NormalAttackState(NormalStateMachine stateMachine) : base("Attack", stateMachine)
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
            SM.ChangeState(SM.runState);
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

        Collider2D collider = Physics2D.OverlapCircle(SM.gameObject.transform.position, SM.monsterData.AttackRange, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            var hp = collider.GetComponent<IHealthPoint>();
            if (hp != null)
            {
                Vector3 hurtPosition = collider.ClosestPoint(SM.transform.position);
                hp.TakeDamage(SM.monsterData.AttackDamage, false, DamageType.physic, hurtPosition);

            }
        }
    }


    private void OnAttackAnimationComplete(TrackEntry trackEntry)
    {

        SM.Flip();
    }
}
