using BehaviorTree;


public class Inverter : Decorator
{

  
    public Inverter()
    { }


   

    public override NodeState Evaluate()
    {
        switch(childNode.Evaluate())
        {
            case NodeState.FAILURE:
                {
                    return NodeState.SUCCESS;
                }
            case NodeState.SUCCESS:
                {
                    return NodeState.FAILURE;
                }
            default :
                {
                    return NodeState.RUNNING;
                }
        }
    }
}
