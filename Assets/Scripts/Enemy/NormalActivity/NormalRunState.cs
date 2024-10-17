using System.Collections;

using UnityEngine;

public class NormalRunState : BaseState
{
    private NormalStateMachine SM;

    public NormalRunState(NormalStateMachine stateMachine) : base("Run", stateMachine)
    {
        SM = stateMachine;
    }

    public override void Enter()
    {       
        base.Enter();      
        SM.StartCoroutine(Run());
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.walk, true).TimeScale = 2.5f;
    }



    IEnumerator Run()
    {             
            SM.Flip();
            Vector2 movedirection = (SM.PlayerPosition.transform.position - SM.transform.position).normalized;
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = movedirection * SM.monsterData.MoveSpeed;
            yield return null;       
    }

    public override void UpdateLogic()
    {   
        base.UpdateLogic();
        
        float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);

        if(SM.isDead != true)
        {
            if (distance <= SM.monsterData.AttackRange )
            {
                SM.ChangeState(SM.rangeAttackState);
            }
            
            else 
            {             
                SM.StartCoroutine(Run());
            }
        }
        else 
        {
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }

    public override void UpdatePhysic()
    {
        base.UpdatePhysic();
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
    }
}
 