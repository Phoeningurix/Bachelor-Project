using AgentLogic;

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
                    brain.ModifyEmotion("fear", 0.1f);
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
                default:
                    break;
            }
        }
    }
}