using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public class BTSequenceNode : BTNode
    {

        public BTSequenceNode(List<BTNode> children) : base(children)
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