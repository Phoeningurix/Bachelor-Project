using System;
using AgentLogic;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool consumable = false;
        public int usesLeft = 1;
        
        private SpriteRenderer _renderer;
        private Collider2D _collider;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        // TODO: Something for waiting
        public void Invoke(BlobBrain brain, Action onSuccess, Action onFailure)
        {
            if ((!consumable || usesLeft > 0) && GetInteractionStatus(brain))
            {
                onSuccess?.Invoke();
                OnSuccess(brain);
                if (consumable && --usesLeft <= 0)
                {
                    _renderer.enabled = false;
                    _collider.enabled = false;
                }
                
            }
            else
            {
                onFailure?.Invoke();
                OnFailure(brain);
            }
            
        }

        protected virtual bool GetInteractionStatus(BlobBrain brain)
        {
            return true;
        }

        protected virtual void OnSuccess(BlobBrain brain)
        {
            
        }

        protected virtual void OnFailure(BlobBrain brain)
        {
            
        }
    }
}