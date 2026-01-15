using UnityEngine;
using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public class BTSelectorNode : BTNode
    {

        public BTSelectorNode(List<BTNode> children) : base(children)
        {
        }

        public override NodeState Tick()
        {
            if (CurrentChildIndex >= ChildrenCount)
            {
                Reset();
                return NodeState.Failure;
            }

            switch (CurrentChild.Tick())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    Reset();
                    return NodeState.Success;
                default:  // Failure Case!
                    CurrentChildIndex++;
                    return NodeState.Running;
            }
        }
    }
}