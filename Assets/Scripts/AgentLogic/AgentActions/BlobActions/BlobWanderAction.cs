using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobWanderAction : AgentAction
    {
        private readonly BlobBrain _agent;

        public BlobWanderAction(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override bool Tick()
        {
            float happiness = _agent.emotions.GetBetween01("happiness");
            float speed = _agent.Blackboard.Get<float>("wanderSpeed") * Mathf.Lerp(0.5f, 1.5f, happiness);
            
            Vector3 target = _agent.Blackboard.Get<Vector3>("wanderTarget");
            
            _agent.transform.position = Vector3.MoveTowards(_agent.transform.position,
                target,
                speed * _agent.DeltaTime()
            );
            if (Vector3.Distance(_agent.transform.position, target) < 0.01f)
            {
                return true;
            }

            return false;
        }
    }
}