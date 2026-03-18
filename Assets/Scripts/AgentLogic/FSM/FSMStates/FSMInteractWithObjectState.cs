using Interactions;
using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMInteractWithObjectState : IState
    {
        private BlobBrain _brain;
        private Interactable _targetObject;

        public FSMInteractWithObjectState(BlobBrain brain)
        {
            _brain = brain;
        }

        public void Tick()
        {
            _brain.NavMeshAgent.speed = DecisionUtils.GetSpeed(_brain);
        }

        public void OnEnter()
        {
             _targetObject = _brain.interactionLocator.GetClosestInteractableInRange(
                    _brain.Blackboard.Get<float>("objectVisibilityRadius"));

             if (_targetObject != null)
             {
                 _brain.Blackboard.Set("targetObject", _targetObject);
                 _brain.NavMeshAgent.enabled = true;
                 _brain.NavMeshAgent.SetDestination(_targetObject.transform.position);
             }
             
            
        }

        public void OnExit()
        {
            _brain.NavMeshAgent.enabled = false;
        }
    }
}