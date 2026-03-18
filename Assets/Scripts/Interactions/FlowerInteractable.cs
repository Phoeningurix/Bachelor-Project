using AgentLogic;
using UnityEngine;

namespace Interactions
{
    public class FlowerInteractable : Interactable
    {
        protected override bool GetInteractionStatus(BlobBrain brain)
        {
            return true;
        }

        protected override void OnSuccess(BlobBrain brain)
        {
            brain.Blackboard.Set("flowers", brain.Blackboard.Get<int>("flowers") + 1);
        }
    }
}