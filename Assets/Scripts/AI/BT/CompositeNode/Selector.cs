using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Composite
{
    public Selector(Node parentNode = null) : base(parentNode) { }

    public Selector(List<Node> nodes, Node parentNode = null) : base(nodes, parentNode) { }

    public override NodeState Evaluate()
    {
        bool isAnyRunning = false;

        foreach (var node in nodes)
        {
            NodeState state = node.Evaluate();

            switch (state)
            {
                case NodeState.SUCCESS:
                    return NodeState.SUCCESS;
                  

                case NodeState.RUNNING:
                    isAnyRunning = true;
                    break;

                case NodeState.FAILURE:
                    continue;
            }
        }
        return isAnyRunning ? NodeState.RUNNING : NodeState.FAILURE;
    }
}
