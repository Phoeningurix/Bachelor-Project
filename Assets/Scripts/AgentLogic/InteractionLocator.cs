using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace AgentLogic
{
    public class InteractionLocator : MonoBehaviour
    {
        private Collider2D[] results = new Collider2D[100];
        
        public List<Interactable> FindInteractablesInRange(float radius)
        {
            List<Interactable> foundInteractables = new List<Interactable>();
 
            int numFound = Physics2D.OverlapCircleNonAlloc(transform.position, radius, results, ~0);

            for (int i = 0; i < numFound; i++)
            {
                if (results[i].gameObject == gameObject) continue;
                
                if (results[i].TryGetComponent(out Interactable interactable))
                {
                    foundInteractables.Add(interactable);
                }
            }

            return foundInteractables;
        }
    }
}