using BehaviorTree;

public class Decorator : Node
{
    public Node childNode;

    public Decorator(Node parentNode = null) : base(parentNode)
    {
    }

    public void setChild(Node child)
    {
        this.childNode = child;
    }
    
    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
}
