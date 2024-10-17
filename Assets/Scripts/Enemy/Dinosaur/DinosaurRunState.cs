using System.Collections;

using UnityEngine;

public class DinosaurRunState : BaseState
{
    private DinosaurStateMachine SM;

    public DinosaurRunState(DinosaurStateMachine stateMachine) : base("Run", stateMachine)
    {
        SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
      
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.walk, true).TimeScale = 2.5f;
    }


  

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!SM.isDead)
        {
            SM.Flip();
            Vector2 moveDirection = (SM.PlayerPosition.transform.position - SM.transform.position).normalized;
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection * SM.monsterData.MoveSpeed;

            float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);
            if (distance <= SM.monsterData.AttackRange)
            {
                SM.ChangeState(SM.attackState);
            }
        }
        else
        {
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public override void UpdatePhysic()
    {
        base.UpdatePhysic();
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
}
