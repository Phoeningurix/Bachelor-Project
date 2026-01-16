using AgentLogic.BehaviorTree;
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
            }
            
            _timeSinceStart += Time.fixedDeltaTime;

            if (_timeSinceStart > _agent.Blackboard.Get<float>("wanderTime"))
            {
                _isRunning = false;
                return true;
            }

            return false;
        }
    }
}