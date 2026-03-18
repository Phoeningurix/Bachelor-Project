using System;
using System.Collections;
using AgentLogic;
using UnityEngine;

namespace Interactions
{
    public abstract class InteractableWithWait : Interactable
    {
        public float interactionDelay = 2f;

        public override void Invoke(BlobBrain brain, Action onSuccess, Action onFailure)
        {
            StartCoroutine(DelayedInvoke(brain, onSuccess, onFailure));
        }

        private IEnumerator DelayedInvoke(BlobBrain brain, Action onSuccess, Action onFailure)
        {
            yield return new WaitForSeconds(interactionDelay);

            base.Invoke(brain, onSuccess, onFailure);
        }
    }
}