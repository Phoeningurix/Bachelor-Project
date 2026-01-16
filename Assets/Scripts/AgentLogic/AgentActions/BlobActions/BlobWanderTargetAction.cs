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
            float openness = (_agent.personalityTraits["openness"].Value + 1f) / 2f;
            float radius = Mathf.Lerp(1f, 3f, openness);
            

            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 target = _agent.transform.position + new Vector3(dir.x, dir.y, 0f) * radius;

            _agent.Blackboard.Set("wanderTarget", target);
            return true;
        }
    }
}