using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobSetTargetObjectAction : AgentAction
    {
        private readonly BlobBrain _agent;

        public BlobSetTargetObjectAction(BlobBrain agent)
        {
            _agent = agent;
        }

        public override bool Tick()
        {
            Interactable nearestObject =
                _agent.interactionLocator.GetClosestInteractableInRange(
                    _agent.Blackboard.Get<float>("objectVisibilityRadius"));
            
            if (nearestObject != null)
            {
                _agent.Blackboard.Set("targetObject", nearestObject);
                
                _agent.NavMeshAgent.enabled = true;
                _agent.NavMeshAgent.SetDestination(nearestObject.transform.position);
                // Debug.Log("New target object:" + _agent.NavMeshAgent.destination);
            }
            
            return true;
            
            
        }
    }
}