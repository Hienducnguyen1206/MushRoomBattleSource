using Spine;
using Spine.Unity;
using UnityEngine;

public class DinosaurIdleState : BaseState
{
    private DinosaurStateMachine SM;
    TrackEntry trackEntry;

    public DinosaurIdleState(DinosaurStateMachine stateMachine) : base("Idle", stateMachine)
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
           while (!trackEntry.IsComplete )
            {   if(distance >= 1.5f)
                {
                    break;
                }
                 return;
            }

           
            if (distance <= SM.monsterData.AttackRange)
            {
                SM.ChangeState(SM.attackState);
            }
            else
            {
                SM.ChangeState(SM.runState);
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

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
       
    }
}