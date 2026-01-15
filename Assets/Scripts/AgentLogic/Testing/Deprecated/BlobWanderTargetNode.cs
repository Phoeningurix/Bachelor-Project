using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.Testing.Deprecated
{
    public class BlobWanderTargetNode : BTNode
    {
        private readonly BlobBrain _agent;

        public BlobWanderTargetNode(BlobBrain agent)
        {
            _agent = agent;
        }

        public override NodeState Tick()
        {
            if (_agent.HasWanderTarget) return NodeState.Success;
            
            float openness = (_agent.personalityTraits["openness"].Value + 1f) / 2f;
            float radius = Mathf.Lerp(1f, 3f, openness);
            

            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 target = _agent.transform.position + new Vector3(dir.x, dir.y, 0f) * radius;

            _agent.wanderTarget = target;

            Debug.Log("Wander target: " + target);
                            
            
            _agent.HasWanderTarget = true;
            
            return NodeState.Success;
        }
    }
}