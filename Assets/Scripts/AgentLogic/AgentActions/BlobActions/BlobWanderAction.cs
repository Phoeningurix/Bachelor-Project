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
            
            _agent.NavMeshAgent.speed = speed;
            
            //Vector3 target = _agent.Blackboard.Get<Vector3>("wanderTarget");
            
            /*_agent.transform.position = Vector3.MoveTowards(_agent.transform.position,
                target,
                speed * _agent.DeltaTime()
            );*/
            
            if (_agent.NavMeshAgent.enabled 
                && !_agent.NavMeshAgent.pathPending 
                && _agent.NavMeshAgent.remainingDistance <= _agent.NavMeshAgent.stoppingDistance)
            {
                _agent.NavMeshAgent.enabled = false;
                return true;
            }

            return false;
        }
    }
}