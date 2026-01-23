using UnityEngine;
using UnityEngine.AI;

namespace AgentLogic.FSM
{
    public class FSMWanderState : IState
    {
        private readonly BlobBrain _brain;
        private readonly NavMeshAgent _navMeshAgent;

        public float TimeStuck;
        private Vector3 _lastPosition = Vector3.zero;

        public FSMWanderState(BlobBrain brain)
        {
            _brain = brain;
            _navMeshAgent = brain.NavMeshAgent;
            // Eventuelle Animationen einfügen
        }
        
        public void Tick()
        {
            float happiness = _brain.emotions.GetBetween01("happiness");
            float speed = _brain.Blackboard.Get<float>("wanderSpeed") * Mathf.Lerp(0.5f, 1.5f, happiness);
            
            /*Vector3 target = _brain.Blackboard.Get<Vector3>("wanderTarget");
            
            _brain.transform.position = Vector3.MoveTowards(_brain.transform.position,
                target,
                speed * _brain.DeltaTime()
            );*/

            _navMeshAgent.speed = speed;
            
            _lastPosition = _brain.transform.position;

            if (Vector3.Distance(_lastPosition, _brain.transform.position) >= 0f)
            {
                TimeStuck += _brain.DeltaTime();
            }
        }

        public void OnEnter()
        {
            // Target berechnen
            float radius = Mathf.Lerp(3f, 9f, _brain.personalityTraits.GetBetween01("openness"));
            
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 target = _brain.transform.position + new Vector3(dir.x, dir.y, 0f) * radius;

            _navMeshAgent.enabled = true;
            //_navMeshAgent.SetDestination(new Vector3(-5f, 3f, _brain.transform.position.z));
            _navMeshAgent.SetDestination(target);
            _brain.Blackboard.Set("wanderTarget", _navMeshAgent.destination);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            
        }
    }
}