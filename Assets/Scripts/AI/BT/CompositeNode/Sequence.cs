using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Composite
{
    public Sequence(Node parentNode = null) : base(parentNode) { }

    public Sequence(List<Node> nodes, Node parentNode = null) : base(nodes, parentNode) { }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            var state = node.Evaluate();

            switch (state)
            {
                case NodeState.SUCCESS:
                  
                    continue;
                case NodeState.RUNNING:
               
                    return NodeState.RUNNING;
                case NodeState.FAILURE:
                   
                    return NodeState.FAILURE;
                default:
                    break;
            }
        }
        return NodeState.SUCCESS;
    }
}
