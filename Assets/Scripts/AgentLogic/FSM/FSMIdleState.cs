
using UnityEngine;

namespace AgentLogic.FSM
{
    public class FSMIdleState : IState
    {
        private readonly BlobBrain _brain;

        private bool _isIdle;

        public FSMIdleState(BlobBrain brain)
        {
            _brain = brain;
        }
        
        public void Tick()
        {
            float timeSinceStart = _brain.Blackboard.Get<float>("idleTimeSinceStart");
            _brain.Blackboard.Set("idleTimeSinceStart", timeSinceStart + Time.fixedDeltaTime);
            
        }

        public bool IsIdle()
        {
            return _isIdle;
        }

        public void OnEnter()
        {
            _isIdle = true;
            _brain.Blackboard.Set("idleTimeSinceStart", 0f);
        }

        public void OnExit()
        {
            _isIdle = false;
        }
    }
}