
using Interactions;
using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMReachedTargetObjectState : IState
    {
        private BlobBrain _brain;

        private bool _interactionFinished = false;
        
        public bool InteractionFinished => _interactionFinished;

        public FSMReachedTargetObjectState(BlobBrain brain)
        {
            _brain = brain;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _interactionFinished = false;
            _brain.Blackboard.Get<Interactable>("targetObject").Invoke(_brain, () =>
            {
                _brain.ModifyEmotion("happiness", 0.1f);
                _interactionFinished = true;
                //Debug.Log("Picked up object");
            }, () =>
            {
                _brain.ModifyEmotion("happiness", -0.1f);
                _interactionFinished = true;
                //Debug.Log("Failed to pick up object");
            });
        }

        public void OnExit()
        {
        }
    }
}