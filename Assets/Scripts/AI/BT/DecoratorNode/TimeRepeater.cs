using BehaviorTree;
using UnityEngine;

public class TimeRepeater : Decorator
{
    private int numberRepeat;  
    private int currentRepeat = 0;  
    private float waitTime;  
    private float elapsedTime = 0;  

    public TimeRepeater(int numberRepeat, float waitTime)
    {
        this.numberRepeat = numberRepeat;
        this.waitTime = waitTime;
    }

 
    public void ResetRepeater()
    {
        currentRepeat = 0;
        elapsedTime = 0;
    }

 
    public override NodeState Evaluate()
    {
     
        if (currentRepeat >= numberRepeat)
        {
            return NodeState.SUCCESS;
        }

       
        elapsedTime += Time.deltaTime;

      
        if (elapsedTime < waitTime)
        {
            return NodeState.RUNNING;
        }

   
        elapsedTime = 0;


        NodeState childState = childNode.Evaluate();

   
        if (childState == NodeState.RUNNING)
        {
            return NodeState.RUNNING;
        }


        if (childState == NodeState.SUCCESS || childState == NodeState.FAILURE)
        {
            currentRepeat++;
            if (currentRepeat < numberRepeat)
            {
                return NodeState.RUNNING;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
