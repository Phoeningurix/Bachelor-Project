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
            AdjustEmotions(request);
            return true;
        }

        private void AdjustEmotions(BlobInteraction interaction)
        {
            //TODO schauen ob man den Sender kennt -> je nach Openness die happiness und fear ändern

            switch (interaction.Message)
            {
                case BlobInteractionType.Greeting: //TODO genauer definieren * _agent.personalityTraits["extraversion"].Value
                    _agent.ModifyEmotion("happiness", 0.2f);
                    _agent.ModifyEmotion("fear", -0.1f);
                    break;
                case BlobInteractionType.Insult:
                    _agent.ModifyEmotion("happiness", -0.2f);
                    _agent.ModifyEmotion("anger", 0.3f);
                    break;
                case BlobInteractionType.Compliment:
                    _agent.ModifyEmotion("happiness", 0.3f);
                    _agent.ModifyEmotion("fear", -0.1f);
                    _agent.ModifyEmotion("anger", -0.1f);
                    break;
                case BlobInteractionType.Gift:
                    _agent.ModifyEmotion("happiness", 0.4f);
                    _agent.ModifyEmotion("fear", -0.2f);
                    _agent.ModifyEmotion("anger", -0.2f);
                    _agent.Blackboard.Set("flowers", _agent.Blackboard.Get<int>("flowers") + 1);
                    break;
                case BlobInteractionType.Scream:
                    _agent.ModifyEmotion("happiness", -0.2f);
                    _agent.ModifyEmotion("fear", 0.3f);
                    _agent.ModifyEmotion("anger", 0.1f);
                    break;
            }
        }
        
    }
}