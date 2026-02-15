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
            List<Interactable> interactables = _agent
                .interactionLocator
                .FindInteractablesInRange(_agent.Blackboard.Get<float>("objectVisibilityRadius"));
            
            Vector3 position = _agent.transform.position;

            // Get the nearest Object
            // TODO: Optimise for Personality Traits (Openness could choose a random object instead of the nearest)
            Interactable nearestObject = null;
            float minDistance = float.MaxValue;
            foreach (Interactable interactable in interactables)
            {
                float distance = Vector3.Distance(position, interactable.transform.position);
                if (distance < minDistance)
                {
                    nearestObject = interactable;
                    minDistance = distance;
                }
            }
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