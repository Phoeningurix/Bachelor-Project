using Testing;
using UnityEngine;

namespace AgentLogic.Testing.BehaviorTree
{
    public class BlobStrategy
    {
        public interface Strategy
        {
            Node.Status Process();
            void Reset();
        }

        public class WanderStrategy : Strategy
        {
            private readonly float _wanderRadius;
            private readonly float _wanderTime;
            private Vector3 _wanderTarget;
            private Vector3 _startPosition;
            private float _timeSinceStart;
            private BlobBrain agent;

            public WanderStrategy(float wanderRadius, float wanderTime,  BlobBrain agent)
            {
                _wanderRadius = wanderRadius;
                _wanderTime = wanderTime;
                this.agent = agent;
                
                _timeSinceStart = 0f;
                _startPosition = agent.transform.position;
                
                Vector2 dir = Random.insideUnitCircle.normalized;
    
                _wanderTarget = _startPosition + new Vector3(dir.x, dir.y, 0f) * _wanderRadius;
                Debug.Log("Wander target: " + _wanderTarget);
            }

            public Node.Status Process()
            {
                // _timeSinceStart += deltaTime;
                // agent.transform.position = Vector3.Lerp(_startPosition, _wanderTarget, Mathf.Clamp01(_timeSinceStart / _wanderTime));
                //
                // if (_timeSinceStart >= _wanderTime) return Node.Status.Success;
                return Node.Status.Failure;
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }
        }
        
    }
}