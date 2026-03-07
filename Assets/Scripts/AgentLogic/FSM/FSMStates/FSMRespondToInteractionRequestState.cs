using Interactions.BlobInteractions;
using UnityEngine;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMRespondToInteractionRequestState : IState
    {
        private BlobInteraction _interaction;
        private BlobBrain _brain;

        public FSMRespondToInteractionRequestState(BlobBrain brain)
        {
            _brain = brain;
        }
        
        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            if (_brain.InteractionRequests.Count > 0)
            {
                _interaction = _brain.InteractionRequests[0];
                _brain.InteractionRequests.RemoveAt(0);
            }
            else
            {
                Debug.LogError("No interaction requests available ");
                _interaction = null;
            }
        }
            

        public void OnExit()
        {
            _interaction?.InvokeReact(BlobInteractionUtils.ChooseResponse(_brain, _interaction.Message));
            _brain.Blackboard.Set("lastAgentInteractionCompleted", Time.time);
            
        }
    }
}