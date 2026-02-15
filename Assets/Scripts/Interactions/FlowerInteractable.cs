using AgentLogic;
using UnityEngine;

namespace Interactions
{
    public class FlowerInteractable : InteractableWithWait
    {
        protected override bool GetInteractionStatus(BlobBrain brain)
        {
            return true;
        }

        protected override void OnSuccess(BlobBrain brain)
        {
            brain.Blackboard.Set("flowers", 1);
        }
    }
}