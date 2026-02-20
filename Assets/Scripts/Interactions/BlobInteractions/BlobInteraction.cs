using System;
using AgentLogic;
using Renderers;
using UnityEngine;

namespace Interactions.BlobInteractions
{
    public class BlobInteraction
    {
        public readonly BlobInteractionType Message;
        private readonly Action<BlobInteractionResponseType> _onResponse;

        public readonly BlobBrain Initiator;
        public readonly BlobBrain Reactor;
        public readonly float TimeStamp;

        public BlobInteraction(BlobBrain initiator, BlobBrain reactor, BlobInteractionType message, 
            Action<BlobInteractionResponseType> onResponse)
        {
            Initiator = initiator;
            Reactor = reactor;
            Message = message;
            _onResponse = onResponse;
            TimeStamp = UnityEngine.Time.time;
            Initiator.speechBubble.Display(message);
            Debug.Log(Initiator.name + " is sending a Message \"" + message.ToString() + "\" to " + Reactor.name + ".");
        }
        
        public void InvokeReact(BlobInteractionResponseType responseType)
        {
            Reactor.speechBubble.Display(responseType);
            _onResponse?.Invoke(responseType); 
            Debug.Log(Reactor.name + " is responding \"" + responseType.ToString() + "\" to " + Initiator.name + ".");
        }
    }
}
    