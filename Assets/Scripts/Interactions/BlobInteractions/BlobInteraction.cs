using System;
using AgentLogic;

namespace Interactions.BlobInteractions
{
    public class BlobInteraction
    {
        public readonly BlobInteractionType Message;
        public readonly Action<BlobInteractionResponseType> OnResponse;

        public readonly BlobBrain Initiator;
        public readonly BlobBrain Reactor;
        // TODO TimeStamp

        public BlobInteraction(BlobBrain initiator, BlobBrain reactor, BlobInteractionType message, 
            Action<BlobInteractionResponseType> onResponse)
        {
            Initiator = initiator;
            Reactor = reactor;
            Message = message;
            OnResponse = onResponse;
        }
        
        public void InvokeReact(BlobInteractionResponseType responseType)
        {
            OnResponse?.Invoke(responseType); 
        }
    }
}
    