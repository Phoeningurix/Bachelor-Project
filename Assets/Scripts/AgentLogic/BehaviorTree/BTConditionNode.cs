using System;

namespace AgentLogic.BehaviorTree
{
    public class BTConditionNode : BTNode
    {
        private readonly Func<bool> _condition;

        public BTConditionNode(Func<bool> condition)
        {
            _condition = condition;
        }

        public override NodeState Tick() => _condition.Invoke() ? NodeState.Success : NodeState.Failure;
    }
}