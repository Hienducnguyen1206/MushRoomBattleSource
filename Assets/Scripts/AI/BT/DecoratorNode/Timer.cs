using BehaviorTree;
using UnityEngine;

public class TimeLimit : Decorator
{
    private float delayTime;
    private float elapsedTime = 0;

    public TimeLimit (Node parentNode = null, float delay = 1f) : base(parentNode)
    {
        this.delayTime = delay;
    }

    public void SetRunTime(float time)
    {
        this.delayTime = time;
    }

    public void Reset()
    {
        this.elapsedTime = 0;
    }

    public override NodeState Evaluate()
    {
        if (elapsedTime < delayTime)
        {           
            elapsedTime += Time.deltaTime;
            childNode.Evaluate();
            
            return NodeState.RUNNING;
            
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
