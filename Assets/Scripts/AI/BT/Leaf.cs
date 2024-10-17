using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;
public class Leaf : Node
{
     
    public delegate NodeState LeafFunction();

    public LeafFunction function;

    public Leaf(LeafFunction function)
    {
        SetFunciton(function);
    }

    public void SetFunciton(LeafFunction func)
    {
        this.function = func;
    }
    public override NodeState Evaluate()
    {
        return function();
    }
}
