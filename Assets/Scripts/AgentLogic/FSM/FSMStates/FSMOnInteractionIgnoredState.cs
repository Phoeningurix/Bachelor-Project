using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMOnInteractionIgnoredState : IState
    {
        private readonly BlobBrain _agent;

        public FSMOnInteractionIgnoredState(BlobBrain agent)
        {
            _agent = agent;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log(_agent.name + " is entering FSMOnInteractionIgnoredState");

            // TODO: adjust emotions on ignored
        }

        public void OnExit()
        {
        }
    }
}