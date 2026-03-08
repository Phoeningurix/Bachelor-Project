using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMFinishedResponseState : IState
    {
        private BlobBrain _brain;

        public FSMFinishedResponseState(BlobBrain brain)
        {
            _brain = brain;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            Debug.Log(_brain.name + "is entering FSMFinishedResponseState");

        }

        public void OnExit()
        {
        }
    }
}