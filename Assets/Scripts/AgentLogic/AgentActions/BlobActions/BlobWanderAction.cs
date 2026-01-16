using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobWanderAction : AgentAction
    {
        private readonly BlobBrain _agent;
        private Vector3 _startPosition;
        private float _timeSinceStart;

        public BlobWanderAction(BlobBrain agent)
        {
            _agent = agent;
            _startPosition = agent.transform.position;
        }
        
        public override bool Tick()
        {
            //Debug.Log("Wander Time: " + _wanderTime);
            //Debug.Log("Wander Target: " + _agent.Blackboard.Get<Vector3>("wanderTarget"));
            _timeSinceStart += Time.fixedDeltaTime;
            _agent.transform.position = Vector3.Lerp
            (
                _startPosition, _agent.Blackboard.Get<Vector3>("wanderTarget"),
                Mathf.Clamp01(_timeSinceStart / _agent.Blackboard.Get<float>("wanderTime"))
            );
            if (_timeSinceStart >= _agent.Blackboard.Get<float>("wanderTime"))
            {
                _timeSinceStart = 0f;
                _startPosition = _agent.transform.position;
                return true;
            }

            return false;
        }
    }
}