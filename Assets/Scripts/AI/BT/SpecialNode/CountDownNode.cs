using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownNode :Node
{
    private float elapseTime = 0;
    private float delayTime;

    public CountDownNode( float delayTime)
    {       
        this.delayTime = delayTime;
        elapseTime = delayTime;
    }

    public void Reset()
    {
        elapseTime = 0; 
    }

    public override NodeState Evaluate()
    {
        if(elapseTime < delayTime)
        {
            elapseTime += Time.deltaTime;
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }


}
