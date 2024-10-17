using BehaviorTree;
using System.Collections;
using UnityEngine;

public class TimerCirle : Decorator
{
    private float delayTime;
    private float elapsedTime = 0;

    
    public TimerCirle( float delay = 1f) 
    {
        this.delayTime = delay;
        elapsedTime = delayTime;
    }

   

    public void Reset()
    {
        this.elapsedTime = 0;
    }

    public override NodeState Evaluate()
    {
       if(this.elapsedTime < this.delayTime)
        {
            elapsedTime += Time.deltaTime;
            return NodeState.FAILURE;
        }
        else
        {  

           switch(childNode.Evaluate())
            {              
                case NodeState.SUCCESS:
                    {   
                        Reset();
                        return NodeState.SUCCESS;
                    }
                case NodeState.RUNNING:
                    {
                        return NodeState.RUNNING;
                    }
                default:
                    {
                        return NodeState.FAILURE;
                    }
            }  
        }
    }
}
