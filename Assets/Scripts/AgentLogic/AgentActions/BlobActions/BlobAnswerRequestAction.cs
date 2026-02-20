using System.Collections.Generic;
using Interactions.BlobInteractions;
using Utils;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobAnswerRequestAction : AgentAction
    {
        private BlobBrain _agent;

        public BlobAnswerRequestAction(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public override bool Tick()
        {
            BlobInteraction request = _agent.InteractionRequests[0];
            AdjustEmotions(request);
            ProcessInteractionRequest(request);
            
            return true;
        }

        private void ProcessInteractionRequest(BlobInteraction interaction)
        {
            BlobInteractionResponseType response;

            Dictionary<BlobInteractionResponseType, float> weights = new Dictionary<BlobInteractionResponseType, float>
            {
                
                [BlobInteractionResponseType.Wave] = 0.5f 
                    - _agent.emotions.GetBetween01("anger") * 0.3f 
                    + _agent.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                [BlobInteractionResponseType.ComplimentBack] = 0.3f 
                   - _agent.emotions.GetBetween01("anger") * 0.4f 
                   + _agent.personalityTraits.GetBetween01("agreeableness") * 0.6f,
                [BlobInteractionResponseType.ThankYou] = 0.3f, // TODO Werte eintragen
                [BlobInteractionResponseType.ScreamBack] = 0.3f,
                [BlobInteractionResponseType.InsultBack] = 0.3f,
                
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

        private void AdjustEmotions(BlobInteraction interaction)
        {
            //TODO schauen ob man den Sender kennt -> je nach Openness die happiness und fear ändern

            switch (interaction.Message)
            {
                case BlobInteractionType.Greeting: //TODO genauer definieren * _agent.personalityTraits["extraversion"].Value
                    _agent.ModifyEmotion("happiness", 0.2f);
                    break;
                case BlobInteractionType.Insult:
                    _agent.ModifyEmotion("happiness", -0.2f);
                    _agent.ModifyEmotion("anger", 0.3f);
                    break;
                case BlobInteractionType.Compliment:
                    _agent.ModifyEmotion("happiness", 0.2f);
                    break;
                case BlobInteractionType.Gift:
                    _agent.ModifyEmotion("happiness", 0.4f);
                    _agent.Blackboard.Set("flowers", _agent.Blackboard.Get<int>("flowers") + 1);
                    break;
                case BlobInteractionType.Scream:
                    _agent.ModifyEmotion("happiness", -0.2f);
                    _agent.ModifyEmotion("fear", 0.3f);
                    break;
            }
        }
        
    }
}