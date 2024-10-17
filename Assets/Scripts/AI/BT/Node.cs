
using System.Collections.Generic;



namespace BehaviorTree
{
    public enum NodeState { SUCCESS, FAILURE, RUNNING }
    public abstract class Node
    {
        public Node parentNode;
        public Node(Node parentN = null)
        {
            this.parentNode = parentN;
        }

        public abstract NodeState Evaluate();
    }

    



         

}

