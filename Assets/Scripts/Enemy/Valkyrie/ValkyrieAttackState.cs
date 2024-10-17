using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkyrieAttackState : BaseState
{
    ValkyrieStateMachine SM;
    float distance;
    public ValkyrieAttackState( ValkyrieStateMachine StateMachine) : base("Attack", StateMachine)
    {
        SM = StateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello from attack stage");
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.attack, true).TimeScale = SM.monsterData.AttackPerSecond ;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(!SM.isDead)
        {
            distance = Vector3.Distance(SM.PlayerPosition.position, SM.transform.position);
            if (distance > SM.monsterData.AttackRange)
            {
                SM.ChangeState(SM.rushState);
            }
        }
       
    }

    public override void UpdatePhysic()
    {
        base.UpdatePhysic();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
