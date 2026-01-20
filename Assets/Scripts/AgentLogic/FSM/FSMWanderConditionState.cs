using UnityEngine;

namespace AgentLogic.FSM
{
    public class FSMWanderConditionState : IState
    {
        private bool _canWander;
        private readonly BlobBrain _brain;

        public FSMWanderConditionState(BlobBrain brain)
        {
            _brain = brain;
        }


        public void Tick()
        {
            if (Random.value <= Mathf.Clamp01(_brain.emotions["happiness"].Value / 2f + 0.5f))
            {
                _canWander = true;
            }
            else
            {
                _canWander = false;
            }
        }

        public bool CanWander()
        {
            return _canWander;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}