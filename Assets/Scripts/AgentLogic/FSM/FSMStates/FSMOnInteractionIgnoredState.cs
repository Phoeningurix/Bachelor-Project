using Interactions.BlobInteractions;
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
            BlobInteractionUtils.OnIgnoredAdjustEmotions(_agent);
        }

        public void OnExit()
        {
        }
    }
}