using Spine;
using System.Collections;

using UnityEngine;

public class GhostIdleState : BaseState
{
    private GhostStateMachine SM;
    TrackEntry trackEntry;
    public GhostIdleState(GhostStateMachine stateMachine) : base("Idle", stateMachine)
    {
        SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        trackEntry = SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.idle, false);
        trackEntry.TimeScale = 2.5f;
    }



    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!SM.isDead)
        {
            SM.Flip();
            float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);
            while (!trackEntry.IsComplete)
            {
                if (distance >= SM.monsterData.AttackRange+1.5f)
                {
                    break;
                }
                return;
            }


            if (distance <= SM.monsterData.AttackRange)
            {
                SM.ChangeState(SM.rangeAttackState);
            }
            else
            {
                SM.ChangeState(SM.flyState);
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
