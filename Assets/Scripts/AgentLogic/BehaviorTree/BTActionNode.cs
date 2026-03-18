
using System;
using AgentLogic.AgentActions;
using UnityEngine;

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
            catch (Exception e)
            {
                Debug.LogWarning($"Failed ticking action '{_action}'. Message: {e.Message}");
                return NodeState.Failure;
            }
        }
    }
}