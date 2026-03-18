using System.Collections.Generic;
using Interactions.BlobInteractions;
using UnityEngine;
using Utils;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobAnswerRequestAction : AgentAction
    {
        private BlobBrain _agent;

        private BlobInteraction _request = null;

        public BlobAnswerRequestAction(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override bool Tick()
        {
            if (_request == null) 
            {
                _request = _agent.InteractionRequests[0];
            }
            else if (Time.time - _request.TimeStamp > _agent.Blackboard.Get<float>("agentResponseWaitTime"))
            {
                ProcessInteractionRequest(_request);
                _request = null;
                _agent.Blackboard.Set("lastAgentInteractionCompleted", Time.time);
                return true;
            }
            
            return false;
        }

        private void ProcessInteractionRequest(BlobInteraction interaction)
        {
            
            _agent.InteractionRequests[0].InvokeReact(BlobInteractionUtils.ChooseResponseType(_agent, interaction.Message));
        }
    }
}