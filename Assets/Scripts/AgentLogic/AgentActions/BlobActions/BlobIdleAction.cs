
using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobIdleAction : AgentAction
    {
        private float _timeSinceStart;
        private bool _isRunning;
        private readonly BlobBrain _agent;

        public BlobIdleAction(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override bool Tick()
        {
            if (!_isRunning)
            {
                //Debug.Log("Blob is idle");
                _timeSinceStart = 0f;
                _isRunning = true;
                // _agent.NavMeshAgent.SetDestination(_agent.transform.position);
            }
            
            _timeSinceStart += _agent.DeltaTime();

            if (_timeSinceStart > _agent.Blackboard.Get<float>("waitTime"))
            {
                _isRunning = false;
                return true;
            }

            return false;
        }
    }
}