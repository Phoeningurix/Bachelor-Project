using AgentLogic.AgentActions.BlobActions;
using UnityEngine;

namespace AgentLogic.FSM
{
    public class FSMWanderState : IState
    {
        private readonly BlobBrain _brain;
        private BlobWanderAction _wanderAction;

        public FSMWanderState(BlobBrain brain)
        {
            _brain = brain;
            _wanderAction = new BlobWanderAction(brain);
        }
        
        public void Tick()
        {
            _wanderAction.Tick();
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            _brain.Blackboard.Set("wanderTarget", Vector3.zero);
        }
    }
}