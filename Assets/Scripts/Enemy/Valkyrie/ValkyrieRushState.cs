using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkyrieRushState : BaseState
{
    private ValkyrieStateMachine SM;
    private Rigidbody2D rb;
    Vector3 direction;
    float distance;
    public ValkyrieRushState(ValkyrieStateMachine StateMachine): base("Rush",StateMachine)
    {
        SM = StateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Rush State");
        rb = SM.gameObject.GetComponent<Rigidbody2D>();
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.walk, true).TimeScale = SM.monsterData.MoveSpeed*1.7f ;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        distance = Vector3.Distance(SM.PlayerPosition.position, SM.transform.position);
        if (rb != null)
        {
            if (!SM.isDead)
            {   
                if(distance > SM.monsterData.AttackRange)
                {
                    direction = (SM.PlayerPosition.position - SM.transform.position).normalized;
                    rb.velocity = direction * SM.monsterData.MoveSpeed * 1.8f;
                    SM.Flip();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    SM.ChangeState(SM.attackState);
                }
                


            }
            else
            {
                rb.velocity = Vector3.zero;
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
