using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobWanderAction : AgentAction
    {
        private readonly BlobBrain _agent;
        private Vector3 _startPosition;
        private float _timeSinceStart;
        private readonly float _wanderTime;

        public BlobWanderAction(BlobBrain agent, float wanderTime = 2f)
        {
            _agent = agent;
            _wanderTime = wanderTime;
            _startPosition = agent.transform.position;
        }
        
        public override bool Tick()
        {
            _timeSinceStart += Time.fixedDeltaTime;
            _agent.transform.position = Vector3.Lerp(_startPosition, _agent.wanderTarget, Mathf.Clamp01(_timeSinceStart / _wanderTime));
            if (_timeSinceStart >= _wanderTime)
            {
                _agent.HasWanderTarget = false;
                _timeSinceStart = 0f;
                _startPosition = _agent.transform.position;
                return true;
            }

            return false;
        }
    }
}