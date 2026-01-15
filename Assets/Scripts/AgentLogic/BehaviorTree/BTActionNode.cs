
using AgentLogic.AgentActions;

namespace AgentLogic.BehaviorTree
{
    public class BTActionNode : BTNode
    {
        private readonly AgentAction _action;

        public BTActionNode(AgentAction action)
        {
            _action = action;
        }

        public override NodeState Tick()
        {
            try
            {
                return _action.Tick() ? NodeState.Success : NodeState.Running;
            }
            catch
            {
                return NodeState.Failure;
            }
        }
    }
}