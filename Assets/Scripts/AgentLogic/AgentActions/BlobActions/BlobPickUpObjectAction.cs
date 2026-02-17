using Interactions;
using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobPickUpObjectAction : AgentAction
    {
        private readonly BlobBrain _agent;

        public BlobPickUpObjectAction(BlobBrain agent)
        {
            _agent = agent;
        }

        public override bool Tick()
        {
            Interactable targetObject = _agent.Blackboard.Get<Interactable>("targetObject");
            targetObject.Invoke(_agent, () =>
            {
                _agent.ModifyEmotion("happiness", 0.1f);
                //_agent.Blackboard.Set("hasObject", true);
                Debug.Log("Picked up object");
            }, () =>
            {
                _agent.ModifyEmotion("happiness", -0.1f);
                Debug.Log("Failed to pick up object");
            });
            return true; // TODO: what?
        }
    }
}