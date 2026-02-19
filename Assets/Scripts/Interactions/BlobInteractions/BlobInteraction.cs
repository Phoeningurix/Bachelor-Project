using System;
using AgentLogic;
using UnityEngine;

namespace Interactions.BlobInteractions
{
    public class BlobInteraction
    {
        public readonly BlobInteractionType Message;
        private readonly Action<BlobInteractionResponseType> OnResponse;

        public readonly BlobBrain Initiator;
        public readonly BlobBrain Reactor;
        public readonly float TimeStamp;

        public BlobInteraction(BlobBrain initiator, BlobBrain reactor, BlobInteractionType message, 
            Action<BlobInteractionResponseType> onResponse)
        {
            Initiator = initiator;
            Reactor = reactor;
            Message = message;
            OnResponse = onResponse;
            TimeStamp = UnityEngine.Time.time;
            Debug.Log(Initiator.name + " is sending a Message \"" + message.ToString() + "\" to " + Reactor.name + ".");
        }
        
        public void InvokeReact(BlobInteractionResponseType responseType)
        {
            OnResponse?.Invoke(responseType); 
            Debug.Log(Reactor.name + " is responding \"" + responseType.ToString() + "\" to " + Initiator.name + ".");
        }
    }
}
    