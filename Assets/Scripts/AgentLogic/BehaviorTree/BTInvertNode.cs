using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public class BTInvertNode : BTNode
    {
        public BTInvertNode(BTNode child) : base(new List<BTNode>{child})
        {
        }
        
        public override NodeState Tick()
        {
            switch (CurrentChild.Tick())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    return NodeState.Failure;
                default: // Failure case
                    return NodeState.Success;
            }
        }
    }

    public static class BTInvertNodeExtensions
    {
        public static BTNode Invert(this BTNode node) => new BTInvertNode(node);
    }
}