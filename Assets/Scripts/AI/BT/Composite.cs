using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Composite : Node
{
    public List<Node> nodes;

    public Composite(Node parentNode = null) : base(parentNode)
    {
        nodes = new List<Node>();
    }

    public Composite(List<Node> nodes, Node parentNode = null) : base(parentNode)
    {
        this.nodes = nodes;
    }

    public void AddChild(Node node)
    {
        nodes.Add(node);
    }

    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
}