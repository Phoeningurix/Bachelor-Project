using UnityEngine;

namespace AgentLogic.Testing
{
    /*public class BlobWanderAction: AgentAction<BlobBrain>
    {
        private readonly float _wanderRadius;
        private readonly float _wanderTime;
        private Vector3 _wanderTarget;
        private Vector3 _startPosition;
        private float _timeSinceStart;
        
        public BlobWanderAction(float wanderRadius, float wanderTime)
        {
            _wanderRadius = wanderRadius;
            _wanderTime = wanderTime;
        }
        
        public override void OnStart(BlobBrain agent)
        {
            _timeSinceStart = 0f;
            
            _startPosition = agent.transform.position;
            
            Vector2 dir = Random.insideUnitCircle.normalized;

            _wanderTarget = _startPosition + new Vector3(dir.x, dir.y, 0f) * _wanderRadius;
            Debug.Log("Wander target: " + _wanderTarget);
        }

        public override void OnStop(BlobBrain agent)
        {
            // No
        }

        public override bool Tick(BlobBrain agent, float deltaTime)
        {
            _timeSinceStart += deltaTime;
            agent.transform.position = Vector3.Lerp(_startPosition, _wanderTarget, Mathf.Clamp01(_timeSinceStart / _wanderTime));
            return _timeSinceStart >= _wanderTime;
        }
    }*/
}