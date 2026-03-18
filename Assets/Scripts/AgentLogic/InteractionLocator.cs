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
            
            //Debug.Log("numFound: " + numFound);
            
            //Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, radius);
            
            for (int i = 0; i < numFound; i++)
            {
                if (_results[i].gameObject == gameObject) continue;
                
                //Debug.Log(_results[i].name);
                
                if (_results[i].TryGetComponent(out BlobBrain brain))
                {
                    foundBlobBrains.Add(brain);
                }
            }
            
            //Debug.Log("foundBlobBrains: " + foundBlobBrains.Count);
            
            return foundBlobBrains;
        }

        public bool IsNearAgents(float radius)
        {
            return FindBlobBrainsInRange(radius).Count > 0;
        }

        public bool IsNearObjects(float radius)
        {
            return FindInteractablesInRange(radius).Count > 0;
        }

        public Interactable GetClosestInteractableInRange(float radius)
        {
            List<Interactable> interactables =
                FindInteractablesInRange(radius);
            
            Vector3 position = transform.position;

            // Get the nearest Object
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
            
            return nearestObject;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            foreach (var result in _results)
            {
                if (result != null) Gizmos.DrawWireSphere(result.transform.position, 2);
            }
        }
    }
}