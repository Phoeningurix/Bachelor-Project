using System;
using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public class BTWeightedSequenceNode : BTWeightedNode
    {

        public BTWeightedSequenceNode(Func<float> weightGetter, List<BTNode> children) : base(weightGetter, children)
        {
        }

        public override NodeState Tick()
        {
            if (CurrentChildIndex >= ChildrenCount)
            {
                Reset();
                return NodeState.Success;
            }

            switch (CurrentChild.Tick())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    CurrentChildIndex++;
                    return NodeState.Running;
                default:  // Failure Case!
                    Reset();
                    return NodeState.Failure;
            }
        }
    }
}