using AgentLogic;

namespace Interactions.BlobInteractions
{
    public class BlobInteractionUtils
    {
        
        // Die Auswirkungen einer Interaction-Response auf den Sender
        public static void DefaultResponseReaction(BlobBrain brain, BlobInteractionResponseType response)
        {
            switch (response)
            {
                case BlobInteractionResponseType.InsultBack:
                    brain.ModifyEmotion("happiness", -0.2f);
                    break;
                case BlobInteractionResponseType.ComplimentBack:
                    brain.ModifyEmotion("happiness", 0.1f);
                    break;
                case BlobInteractionResponseType.ScreamBack:
                    brain.ModifyEmotion("happiness", -0.1f);
                    break;
                default:
                    break;
            }
        }
    }
}