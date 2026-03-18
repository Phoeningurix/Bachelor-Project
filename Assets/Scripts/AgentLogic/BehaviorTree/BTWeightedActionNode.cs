using System;
using AgentLogic.AgentActions;
using UnityEngine;

namespace AgentLogic.BehaviorTree
{
    public class BTWeightedActionNode : BTWeightedNode
    {
        private readonly AgentAction _action;

        public BTWeightedActionNode(AgentAction action, Func<float> weightGetter = null) : base(weightGetter)
        {
            _action = action;
        }

        public override BTNode.NodeState Tick()
        {
            try
            {
                return _action.Tick() ? BTNode.NodeState.Success : BTNode.NodeState.Running;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed weighted ticking action '{_action}'. Message: {e.StackTrace}");
                return BTNode.NodeState.Failure;
            }
        }}
}