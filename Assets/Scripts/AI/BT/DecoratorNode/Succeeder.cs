using BehaviorTree;


public class Succeeder : Decorator
{
    Node child;
    public Succeeder(Node parrentNode) : base(parrentNode){}

    public override NodeState Evaluate()
    {
       if(child == null)
       {
            child.Evaluate();           
       }
        return NodeState.SUCCESS;
    }

}
