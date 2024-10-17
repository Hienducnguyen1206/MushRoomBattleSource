using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkyrieFlyState : BaseState
{
    private ValkyrieStateMachine SM;
    public ValkyrieFlyState(ValkyrieStateMachine stateMachine) : base("Fly",stateMachine)
    {
        SM = stateMachine;
    }
    Vector3 position;
    
    private Rigidbody2D rb;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Fly State");
        
        rb = SM.gameObject.GetComponent<Rigidbody2D>();
        float x, y;
        while (true)
        {
            x = UnityEngine.Random.Range(-SM.MaxX, SM.MaxX);
            y = UnityEngine.Random.Range(-SM.MaxY, SM.MaxY);
            position = new Vector3(x, y, 0);

            if (SM.playzone.OverlapPoint(position)) break;
        }
        SM.skeletonAnimation.AnimationState.SetAnimation(0, SM.walk, true).TimeScale = SM.monsterData.MoveSpeed;
    
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!SM.isDead)
        {
            Vector2 moveDirection = (position - SM.transform.position).normalized;
            rb.velocity = moveDirection * SM.monsterData.MoveSpeed; 
            SM.Flip();
            float distance = Vector2.Distance(SM.PlayerPosition.transform.position, SM.transform.position);
            if (distance <= SM.monsterData.AttackRange*3.5f)
            {   
                rb.velocity = Vector2.zero;
                SM.ChangeState(SM.rushState);
            }
            else if (Vector2.Distance(position,SM.transform.position) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                SM.ChangeState(SM.idleState);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }
}
