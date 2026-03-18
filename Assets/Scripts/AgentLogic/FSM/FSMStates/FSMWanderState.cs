using UnityEngine;
using UnityEngine.AI;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMWanderState : IState
    {
        private readonly BlobBrain _brain;
        private readonly NavMeshAgent _navMeshAgent;

        public FSMWanderState(BlobBrain brain)
        {
            _brain = brain;
            _navMeshAgent = brain.NavMeshAgent;
            // Eventuelle Animationen einfügen
        }
        
        public void Tick()
        {
            _navMeshAgent.speed = DecisionUtils.GetSpeed(_brain);
        }

        public void OnEnter()
        {
            // Target berechnen
            float radius = Mathf.Lerp(1f, 9f, _brain.personalityTraits.GetBetween01("openness"));
            
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