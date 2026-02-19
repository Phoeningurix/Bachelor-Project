using System;
using System.Collections.Generic;
using System.Linq;
using Interactions;
using Unity.VisualScripting;
using UnityEngine;

namespace AgentLogic
{
    public class InteractionLocator : MonoBehaviour
    {
        private Collider2D[] _results = new Collider2D[100];
        
        public List<Interactable> FindInteractablesInRange(float radius)
        {
            List<Interactable> foundInteractables = new List<Interactable>();
 
            int numFound = Physics2D.OverlapCircleNonAlloc(transform.position, radius, _results, ~0);

            for (int i = 0; i < numFound; i++)
            {
                if (_results[i].gameObject == gameObject) continue;
                
                if (_results[i].TryGetComponent(out Interactable interactable))
                {
                    foundInteractables.Add(interactable);
                }
            }

            return foundInteractables;
        }
        
        

        public List<BlobBrain> FindBlobBrainsInRange(float radius)
        {
            
            List<BlobBrain> foundBlobBrains = new List<BlobBrain>();
            
            int numFound = Physics2D.OverlapCircle(
                transform.position,
                radius,
                ContactFilter2D.noFilter,
                _results
            ); 
            
            //Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, radius);
            
            for (int i = 0; i < numFound; i++)
            {
                if (_results[i].gameObject == gameObject) continue;
                
                if (_results[i].TryGetComponent(out BlobBrain brain))
                {
                    foundBlobBrains.Add(brain);
                }
            }
            
            return foundBlobBrains;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            foreach (var result in _results)
            {
                Gizmos.DrawWireSphere(result.transform.position, 2);
            }
        }
    }
}