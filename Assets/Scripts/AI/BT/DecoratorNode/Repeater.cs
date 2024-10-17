using BehaviorTree;
using UnityEngine;

public class Repeater : Decorator
{
    private int numberRepeat;
    private int currentRepeat = 0;  

    public Repeater(int numberRepeat)
    {
        this.numberRepeat = numberRepeat;
    }

  
    public void ResetCurrentRepeat()
    {
        currentRepeat = 0;
    }

    public override NodeState Evaluate()
    {

        if (currentRepeat >= numberRepeat)
        {
            return NodeState.SUCCESS;
        }

  
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
