using System;
using System.Collections.Generic;
using Interactions.BlobInteractions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobAgentInteractionAction : AgentAction
    {
        private float _timeSinceStart;
        private float _waitTime;

        private bool _hasSentRequest;

        private BlobBrain _agent;

        public BlobAgentInteractionAction(BlobBrain agent)
        {
            _agent = agent;
            _timeSinceStart = 0f;
            _waitTime = 10f;
            _hasSentRequest = false;
        }

        public override bool Tick()
        {
            if (_timeSinceStart > _waitTime) return true;

            if (!_hasSentRequest)
            {
                List<BlobBrain> otherAgents = _agent
                    .interactionLocator.FindBlobBrainsInRange(_agent
                        .Blackboard.Get<float>("agentInteractionRadius"));

                if (otherAgents.Count == 0) return true;

                BlobBrain interactionReceiver = otherAgents[0]; //TODO: Oder auf eine andere Art entscheiden
                BlobInteractionType message = BlobInteractionType.Greeting;

                if (Random.value < _agent.personalityTraits.GetBetween01("agreeableness"))
                {
                    if (_agent.Blackboard.Get<int>("flowers") > 0)
                    {
                        BlobInteractionType[] choice =
                        {
                            BlobInteractionType.Greeting,
                            BlobInteractionType.Compliment,
                            BlobInteractionType.Gift
                        };
                        //TODO
                        //Hier bitte auswählen welche Interaction gesendet wird und dann entscheiden,
                        //was die auswirken und was für Responses die haben. 
                    }
                }
                else
                {
                    // negative interaktionen
                }

                // Default Behavior
                Action<BlobInteractionResponseType> onResponse1 = r =>
                {
                    BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                };
                
                // Default Behavior für alle Fälle außer "Thank You"
                Action<BlobInteractionResponseType> onResponse2 = r =>
                {
                    switch (r)
                    {
                        case BlobInteractionResponseType.ThankYou:
                            _agent.ModifyEmotion("happiness", 0.1f);
                            _agent.ModifyEmotion("fear", -0.2f);
                            break;
                        default:
                            BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                            break;
                    }
                };
                
                // Default Behavior für alle, bei "Thank You" zusätzliches Behavior
                Action<BlobInteractionResponseType> onResponse3 = r =>
                {
                    switch (r)
                    {
                        case BlobInteractionResponseType.ThankYou:
                            _agent.ModifyEmotion("happiness", 0.1f);
                            _agent.ModifyEmotion("fear", -0.2f);
                            break;
                    }
                    BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                };
                
                // Default Behavior nur bei Insult
                Action<BlobInteractionResponseType> onResponse4 = r =>
                {
                    switch (r)
                    {
                        case BlobInteractionResponseType.ThankYou:
                            _agent.ModifyEmotion("happiness", 0.1f);
                            _agent.ModifyEmotion("fear", -0.2f);
                            break;
                        case BlobInteractionResponseType.InsultBack:
                            BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                            break;
                    }
                };
                
                // Default Behavior und custom Behavior bei Insult,
                // nur custom Behavior bei Thank You
                // Sonst immer default
                Action<BlobInteractionResponseType> onResponse5 = r =>
                {
                    switch (r)
                    {
                        case BlobInteractionResponseType.ThankYou:
                            _agent.ModifyEmotion("happiness", 0.1f);
                            _agent.ModifyEmotion("fear", -0.2f);
                            break;
                        case BlobInteractionResponseType.InsultBack:
                            BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                            _agent.ModifyEmotion("fear", 0.1f);
                            break;
                        default:
                            BlobInteractionUtils.DefaultResponseReaction(_agent, r);
                            break;
                    }
                };

                
                BlobInteraction interactionRequest = new BlobInteraction(
                    _agent,
                    interactionReceiver,
                    message,
                    response =>
                    {
                        
                    });
                
                

                _hasSentRequest = true;
            }

            _timeSinceStart += Time.deltaTime;
            return false;
        }
    }
}