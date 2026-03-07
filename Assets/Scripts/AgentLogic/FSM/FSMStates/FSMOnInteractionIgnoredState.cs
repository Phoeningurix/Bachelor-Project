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
            Debug.Log("FSMOnInteractionIgnoredState");
            // TODO: adjust emotions on ignored
        }

        public void OnExit()
        {
        }
    }
}