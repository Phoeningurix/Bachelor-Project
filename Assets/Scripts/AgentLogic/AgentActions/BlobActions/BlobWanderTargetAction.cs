using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobWanderTargetAction : AgentAction
    {
        private readonly BlobBrain _agent;

        public BlobWanderTargetAction(BlobBrain agent)
        {
            _agent = agent;
        }

        public override bool Tick()
        {
            float openness = _agent.personalityTraits.GetBetween01("openness");
            float radius = Mathf.Lerp(1f, 9f, openness);
            _agent.Blackboard.Set("wanderTargetRadius", radius);
            

            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 target = _agent.transform.position + new Vector3(dir.x, dir.y, 0f) * radius;
            
            _agent.NavMeshAgent.enabled = true;
            _agent.NavMeshAgent.SetDestination(target);
            
            _agent.Blackboard.Set("wanderTarget", _agent.NavMeshAgent.destination);
            
            return true;
        }
    }
}