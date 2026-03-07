using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMSendInteractionFinishedState : IState
    {
        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("FSM SendInteractionFinishedState");
        }

        public void OnExit()
        {
        }
    }
}