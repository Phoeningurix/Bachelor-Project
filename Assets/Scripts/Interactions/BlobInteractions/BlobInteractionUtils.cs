using System.Collections.Generic;
using AgentLogic;
using Utils;

namespace Interactions.BlobInteractions
{
    public class BlobInteractionUtils
    {
        
        // Die Auswirkungen einer Interaction-Response auf die Emotionen des Senders
        public static void DefaultResponseReaction(BlobBrain brain, BlobInteractionResponseType response)
        {
            switch (response)
            {
                case BlobInteractionResponseType.InsultBack:
                    brain.ModifyEmotion("happiness", -0.2f);
                    brain.ModifyEmotion("anger", 0.3f);
                    break;
                case BlobInteractionResponseType.ComplimentBack:
                    brain.ModifyEmotion("happiness", 0.3f);
                    brain.ModifyEmotion("fear", -0.1f);
                    brain.ModifyEmotion("anger", -0.1f);
                    break;
                case BlobInteractionResponseType.ScreamBack:
                    brain.ModifyEmotion("happiness", -0.2f);
                    brain.ModifyEmotion("fear", 0.3f);
                    brain.ModifyEmotion("anger", 0.1f);
                    break;
                case BlobInteractionResponseType.Wave:
                    brain.ModifyEmotion("fear", -0.1f);
                    brain.ModifyEmotion("happiness", 0.1f);
                    break;
                case BlobInteractionResponseType.ThankYou:
                    brain.ModifyEmotion("happiness", 0.2f);
                    brain.ModifyEmotion("anger", -0.2f);
                    brain.ModifyEmotion("fear", -0.1f);
                    break;
            }
        }

        public static BlobInteractionResponseType ChooseResponseType(BlobBrain brain, BlobInteractionType message)
        {
            Dictionary<BlobInteractionResponseType, float> weights = new Dictionary<BlobInteractionResponseType, float>
            {
                
                [BlobInteractionResponseType.Wave] = 0.5f 
                    - brain.emotions.GetBetween01("anger") * 0.3f 
                    + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                [BlobInteractionResponseType.ComplimentBack] = 0.3f 
                   - brain.emotions.GetBetween01("anger") * 0.4f 
                   + brain.personalityTraits.GetBetween01("agreeableness") * 0.6f
                - brain.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionResponseType.ThankYou] = 0.5f 
                     - brain.emotions.GetBetween01("anger") * 0.3f 
                     + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f, 
                [BlobInteractionResponseType.ScreamBack] = 0.2f 
                   + brain.emotions.GetBetween01("anger") * 0.5f 
                   - brain.personalityTraits.GetBetween01("agreeableness") * 0.4f
                   + brain.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionResponseType.InsultBack] = 0.5f 
                    + brain.emotions.GetBetween01("anger") * 0.3f
                    - brain.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                
            }; 
                
            List<BlobInteractionResponseType> choices = new List<BlobInteractionResponseType>
            {
                BlobInteractionResponseType.ComplimentBack,
                BlobInteractionResponseType.Wave,
                BlobInteractionResponseType.InsultBack,
                BlobInteractionResponseType.ScreamBack,
            };

            switch (message)
            {
                case BlobInteractionType.Gift:
                    choices.Add(BlobInteractionResponseType.ThankYou);
                    break;
                case BlobInteractionType.Compliment:
                    choices.Add(BlobInteractionResponseType.ThankYou);
                    break;
            }
            
            ListUtils.WeightedShuffleInPlace(choices, r => weights[r]);
            return choices[0];
        }

        public static BlobInteractionType ChooseInteractionType(BlobBrain brain)
        {
            Dictionary<BlobInteractionType, float> weights = new Dictionary<BlobInteractionType, float>
            {
                
                [BlobInteractionType.Greeting] = 0.5f 
                     - brain.emotions.GetBetween01("anger") * 0.3f 
                     + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f,
                [BlobInteractionType.Compliment] = 0.3f 
                   - brain.emotions.GetBetween01("anger") * 0.4f 
                   + brain.personalityTraits.GetBetween01("agreeableness") * 0.6f
                   - brain.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionType.Insult] = 0.5f 
                     - brain.emotions.GetBetween01("anger") * 0.3f 
                     + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f, 
                [BlobInteractionType.Scream] = 0.2f 
                   + brain.emotions.GetBetween01("anger") * 0.5f 
                   - brain.personalityTraits.GetBetween01("agreeableness") * 0.4f
                   + brain.personalityTraits.GetBetween01("extraversion") * 0.2f,
                [BlobInteractionType.Gift] = 0.2f 
                 + brain.emotions.GetBetween01("anger") * 0.3f
                 - brain.personalityTraits.GetBetween01("agreeableness") * 0.7f,
            };

            List<BlobInteractionType> choices = new List<BlobInteractionType>
            {
                BlobInteractionType.Compliment,
                BlobInteractionType.Greeting,
                BlobInteractionType.Insult,
                BlobInteractionType.Scream,
            };
            
            if (brain.Blackboard.Get<int>("flowers") > 0)
            {
                choices.Add(BlobInteractionType.Gift);
            }
            
            ListUtils.WeightedShuffleInPlace(choices, r => weights[r]);
            return choices[0];
        }
        
        public static void OnRequestAdjustEmotions(BlobBrain brain, BlobInteractionType message)
        {
            //TODO schauen ob man den Sender kennt -> je nach Openness die happiness und fear ändern

            switch (message)
            {
                case BlobInteractionType.Greeting: //TODO genauer definieren * _agent.personalityTraits["extraversion"].Value
                    brain.ModifyEmotion("happiness", 0.2f);
                    brain.ModifyEmotion("fear", -0.1f);
                    break;
                case BlobInteractionType.Insult:
                    brain.ModifyEmotion("happiness", -0.2f);
                    brain.ModifyEmotion("anger", 0.3f);
                    break;
                case BlobInteractionType.Compliment:
                    brain.ModifyEmotion("happiness", 0.3f);
                    brain.ModifyEmotion("fear", -0.1f);
                    brain.ModifyEmotion("anger", -0.1f);
                    break;
                case BlobInteractionType.Gift:
                    brain.ModifyEmotion("happiness", 0.4f);
                    brain.ModifyEmotion("fear", -0.2f);
                    brain.ModifyEmotion("anger", -0.2f);
                    brain.Blackboard.Set("flowers", brain.Blackboard.Get<int>("flowers") + 1);
                    break;
                case BlobInteractionType.Scream:
                    brain.ModifyEmotion("happiness", -0.2f);
                    brain.ModifyEmotion("fear", 0.3f);
                    brain.ModifyEmotion("anger", 0.1f);
                    break;
            }
        }
    }
}