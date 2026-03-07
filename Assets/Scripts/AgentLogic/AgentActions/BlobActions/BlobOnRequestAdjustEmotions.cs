using System.Collections.Generic;
using Interactions.BlobInteractions;
using UnityEngine;
using Utils;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobOnRequestAdjustEmotions : AgentAction
    {
        private BlobBrain _agent;

        public BlobOnRequestAdjustEmotions(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override bool Tick()
        {
            BlobInteraction request = _agent.InteractionRequests[0];
            BlobInteractionUtils.OnRequestAdjustEmotions(_agent, request.Message);
            return true;
        }
        
    }
}