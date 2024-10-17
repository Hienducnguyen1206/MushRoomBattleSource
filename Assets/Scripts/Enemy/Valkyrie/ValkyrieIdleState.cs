using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValkyrieIdleState : BaseState
{
    private ValkyrieStateMachine SM;

    public ValkyrieIdleState(ValkyrieStateMachine stateMachine):base("Idle",stateMachine)
    {
        SM = stateMachine;
    }



    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Idle State");
        SM.isPatroling = true;
        SM.skeletonAnimation.AnimationState.SetAnimation(0,SM.idle,true);
       
    }
    public override void UpdateLogic()
    {   
        base.UpdateLogic();

        if (SM.isPatroling)
        {
            SM.StartCoroutine(Patrol());
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

    IEnumerator Patrol()
    {   SM.isPatroling = false;
        yield return new WaitForSeconds(2f);
        float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);
        if (!SM.isDead)
        {
            if (distance <= SM.monsterData.AttackRange)
            {
                SM.ChangeState(SM.rushState);
                
            }

            else
            {
                SM.ChangeState(SM.flyState);
               
            }
        }
        else
        {
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }
}
