using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;


public class BT 
{
    public Composite rootNode;
   
   
    public void Tick()
    {
        rootNode.Evaluate();
    }
}
