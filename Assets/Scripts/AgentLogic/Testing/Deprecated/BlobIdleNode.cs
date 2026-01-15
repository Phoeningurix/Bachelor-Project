using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.Testing.Deprecated
{
    public class BlobIdleNode : BTNode
    {
        private readonly float _waitTime;
        private float _timeSinceStart;
        private bool _isRunning;
        private BlobBrain _agent;

        public BlobIdleNode(BlobBrain agent, float waitTime)
        {
            _waitTime = waitTime;
            _agent = agent;
        }
        
        public override NodeState Tick()
        {
            Debug.Log("Blob is idle");
            if (!_isRunning)
            {
                OnStart();
                _isRunning = true;
            }

            _timeSinceStart += Time.fixedDeltaTime;

            if (_timeSinceStart > _waitTime)
            {
                _isRunning = false;
                OnStop();
                return NodeState.Success;
            }
            
            return NodeState.Running;

        }

        private void OnStart()
        {
            _timeSinceStart = 0f;
            //_agent.IsIdle = true;
            // Alles was in der Idle Action noch passieren muss
        }
        
        private void OnStop()
        {
           // Alles beenden, was in der Idle Action passiert
           //_agent.IsIdle = false;
        }
    }
}