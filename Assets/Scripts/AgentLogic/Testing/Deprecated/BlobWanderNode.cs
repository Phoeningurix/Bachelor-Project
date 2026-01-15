using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.Testing.Deprecated
{
    public class BlobWanderNode : BTNode
    {
        
        private readonly BlobBrain _agent;
        private Vector3 _startPosition;
        private float _timeSinceStart;
        private readonly float _wanderTime;
        //private Vector3 _wanderTarget;

        public BlobWanderNode(BlobBrain agent, float wanderTime)
        {
            _agent = agent;
            _wanderTime = wanderTime;
            _startPosition = agent.transform.position;
            //_wanderTarget = _agent.WanderTarget;
        }


        private void OnStart()
        {
            // Alles was in der Wander Action noch passieren muss
        }
        
        private void OnStop()
        {
            // Alles beenden, was in der Wander Action passiert
            _agent.HasWanderTarget = false;
            _timeSinceStart = 0f;
            _startPosition = _agent.transform.position;
        }

        public override NodeState Tick()
        {
            _timeSinceStart += Time.fixedDeltaTime;
            _agent.transform.position = Vector3.Lerp(_startPosition, _agent.wanderTarget, Mathf.Clamp01(_timeSinceStart / _wanderTime));
            if (_timeSinceStart >= _wanderTime)
            {
                OnStop();
                return NodeState.Success;
            }
            return NodeState.Running;
        }
    }
}