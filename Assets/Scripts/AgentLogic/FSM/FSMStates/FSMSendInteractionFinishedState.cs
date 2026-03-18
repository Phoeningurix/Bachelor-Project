using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMSendInteractionFinishedState : IState
    {
        private BlobBrain _brain;

        public FSMSendInteractionFinishedState(BlobBrain brain)
        {
            _brain = brain;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log(_brain.name + "is entering FSMSendInteractionFinishedState");
        }

        public void OnExit()
        {
        }
    }
}