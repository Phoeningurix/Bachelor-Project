using System.Collections.Generic;
using Interactions.BlobInteractions;
using Unity.IntegerTime;
using UnityEngine;
using Utils;

namespace AgentLogic.FSM.FSMStates
{
    public class FSMSendInteractionState : IState
    {
        private readonly BlobBrain _agent;

        public FSMSendInteractionState(BlobBrain agent)
        {
            _agent = agent;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("FSM SendInteractionState");
            _agent.Blackboard.Set("receivedResponse", false);
            _agent.Blackboard.Set("agentInteractionInvoked", Time.time);
            List<BlobBrain> neighbors = _agent.interactionLocator.FindBlobBrainsInRange(_agent.Blackboard.Get<float>("agentInteractionRadius"));
            if (neighbors.Count == 0)
            {
                return;
            }
            ListUtils.ShuffleInPlace(neighbors);
            
            BlobBrain other = neighbors[0];
            
            BlobInteraction interaction = new BlobInteraction(
                _agent, 
                other, 
                BlobInteractionUtils.ChooseInteractionType(_agent),
                response =>
                {
                    BlobInteractionUtils.DefaultResponseReaction(_agent, response);
                    _agent.Blackboard.Set("receivedResponse", true);
                });
            other.RequestInteraction(interaction);
        }

        public void OnExit()
        {
            
        }
    }
}