using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.Testing.Deprecated
{
    public class BlobWanderConditionNode : BTNode
    {
        private BlobBrain _agent;

        public BlobWanderConditionNode(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override NodeState Tick()
        {
            float wanderProbability = Mathf.Clamp01(_agent.emotions["happiness"].Value / 2f + 0.5f);
            if (Random.value <= wanderProbability /*&& !_agent.IsIdle*/)
            {
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}