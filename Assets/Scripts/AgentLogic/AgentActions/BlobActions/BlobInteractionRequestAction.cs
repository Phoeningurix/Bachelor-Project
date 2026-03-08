using System;
using System.Collections.Generic;
using Interactions.BlobInteractions;
using UnityEngine;
using Utils;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobInteractionRequestAction : AgentAction
    {
        private float _timeSinceStart;
        private float _waitTime;

        private bool _hasSentRequest;
        private bool _hasReceivedResponse;
        
        private readonly BlobInteractionType _message;
        private readonly Action<BlobInteractionResponseType> _onResponse;

        private BlobBrain _agent;

        public BlobInteractionRequestAction(BlobBrain agent, BlobInteractionType message, Action<BlobInteractionResponseType> onResponse)
        {
            _agent = agent;
            _timeSinceStart = 0f;
            _waitTime = agent.Blackboard.Get<float>("agentInteractionWaitTime");
            _hasSentRequest = false;
            _hasReceivedResponse = false;
            _message = message;
            _onResponse = onResponse;
        }

        public BlobInteractionRequestAction(BlobBrain agent, BlobInteractionType message) 
            : this(agent, message, r 
                => BlobInteractionUtils.DefaultResponseReaction(agent, r))
        {
            
        }

        public override bool Tick()
        {
            if (_timeSinceStart > _waitTime && !_hasReceivedResponse)
            {
                // On ignored
                _timeSinceStart = 0f;
                _hasSentRequest = false;
                _agent.Blackboard.Set("lastAgentInteractionCompleted", Time.time);
                BlobInteractionUtils.OnIgnoredAdjustEmotions(_agent);
                return true;
            }

            if (_hasReceivedResponse)
            {
                // on response
                _timeSinceStart = 0f;
                _hasSentRequest = false;
                _hasReceivedResponse = false;
                _agent.Blackboard.Set("lastAgentInteractionCompleted", Time.time);
                return true;
            }

            if (!_hasSentRequest)
            {
                List<BlobBrain> otherAgents = _agent
                    .interactionLocator.FindBlobBrainsInRange(_agent
                        .Blackboard.Get<float>("agentInteractionRadius"));

                // Die Action failt, wenn keine Agents mehr in der Nähe sind
                if (otherAgents.Count == 0)
                {
                    _timeSinceStart = 0f;
                    Debug.LogError($"No agents found in {_agent.name}");
                    throw new Exception("No agents found");
                }

                ListUtils.ShuffleInPlace(otherAgents);
                BlobBrain interactionReceiver = otherAgents[0]; 
                
                _hasReceivedResponse = false;
                
                BlobInteraction interactionRequest = new BlobInteraction(
                    _agent,
                    interactionReceiver,
                    _message,
                    response =>
                    {
                        _onResponse?.Invoke(response);
                        _hasReceivedResponse = true;
                    });
                
                interactionReceiver.RequestInteraction(interactionRequest);
                
                _hasSentRequest = true;
            }

            _timeSinceStart += Time.deltaTime;
            return false;
        }
    }
}