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
            Dictionary<BlobInteractionResponseType, float> weights = new Dictionary<BlobInteractionResponseType, float>
            {
                
                [BlobInteractionResponseType.Wave] = 0.5f 
                    - _agent.emotions.GetBetween01("anger") * 0.3f 
                    + _agent.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                [BlobInteractionResponseType.ComplimentBack] = 0.3f 
                   - _agent.emotions.GetBetween01("anger") * 0.4f 
                   + _agent.personalityTraits.GetBetween01("agreeableness") * 0.6f
                - _agent.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionResponseType.ThankYou] = 0.5f 
                     - _agent.emotions.GetBetween01("anger") * 0.3f 
                     + _agent.personalityTraits.GetBetween01("agreeableness") * 0.7f, 
                [BlobInteractionResponseType.ScreamBack] = 0.2f 
                   + _agent.emotions.GetBetween01("anger") * 0.5f 
                   - _agent.personalityTraits.GetBetween01("agreeableness") * 0.4f
                   + _agent.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionResponseType.InsultBack] = 0.5f 
                    + _agent.emotions.GetBetween01("anger") * 0.3f
                    - _agent.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                
            }; 
                
            List<BlobInteractionResponseType> choices = new List<BlobInteractionResponseType>
            {
                BlobInteractionResponseType.ComplimentBack,
                BlobInteractionResponseType.Wave,
                BlobInteractionResponseType.InsultBack,
                BlobInteractionResponseType.ScreamBack,
            };

            switch (interaction.Message)
            {
                case BlobInteractionType.Gift:
                    choices.Add(BlobInteractionResponseType.ThankYou);
                    break;
                case BlobInteractionType.Compliment:
                    choices.Add(BlobInteractionResponseType.ThankYou);
                    break;
            }
            
            ListUtils.WeightedShuffleInPlace(choices, r => weights[r]);
            
            _agent.InteractionRequests[0].InvokeReact(choices[0]);
        }
    }
}