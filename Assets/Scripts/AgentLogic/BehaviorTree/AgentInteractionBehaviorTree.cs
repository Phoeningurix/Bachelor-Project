using System.Collections.Generic;
using AgentLogic.AgentActions.BlobActions;
using Interactions;
using UnityEngine;

namespace AgentLogic.BehaviorTree
{
    public class AgentInteractionBehaviorTree : BehaviorTree
    {
        public AgentInteractionBehaviorTree(BlobBrain brain)
        {
            Root = new BTSelectorNode(new List<BTNode>
            { //TODO als erstes prüfen ob es Requests gibt.
                new BTSequenceNode(new List<BTNode> // Interact with other agents
                {
                    new BTConditionNode(() => brain.interactionLocator
                        .FindBlobBrainsInRange(
                            brain
                                .Blackboard.Get<float>("agentInteractionRadius")).Count > 0),
                    new BTConditionNode(() =>
                    {
                        float probability = 0.3f + brain.emotions["happiness"].Value * 0.1f
                                            + brain.personalityTraits["extraversion"].Value * 0.4f
                                            - brain.emotions["fear"].Value * 0.2f;
                        float r = Random.value;
                        return r < Mathf.Clamp01(probability);
                    }),
                    new BTActionNode(new BlobAgentInteractionAction(brain))
                }),
                new BTSequenceNode(new List<BTNode> // Interact with Objects
                    {
                        new BTConditionNode(() => 
                            brain
                                .interactionLocator
                                .FindInteractablesInRange(
                                    brain.Blackboard.Get<float>("objectVisibilityRadius"))
                                .Count > 0),
                        new BTConditionNode(() =>
                            {
                                if (brain.Blackboard.Get<bool>("hasObject")) return false;
                                float probability = 0.3f - brain.emotions["happiness"].Value * 0.5f
                                                    + brain.personalityTraits["openness"].Value * 0.5f;
                                float r = Random.value;
                                return r < Mathf.Clamp01(probability);
                            }),
                        new BTActionNode(new BlobSetTargetObjectAction(brain)),
                        new BTActionNode(new BlobGoToTargetAction(brain)), 
                        new BTConditionNode(() => Vector3.Distance(brain.transform.position, 
                                                      brain.Blackboard
                                                          .Get<Interactable>("targetObject").transform.position) 
                                                  <= brain.Blackboard
                                                      .Get<float>("objectInteractionRadius")),
                        new BTActionNode(new BlobInteractWithWaitAction(brain, () =>
                        {
                            brain.ModifyEmotion("happiness", 0.1f);
                            //_agent.Blackboard.Set("hasObject", true);
                            Debug.Log("Picked up object");
                        }, () =>
                        {
                           brain.ModifyEmotion("happiness", -0.1f);
                            Debug.Log("Failed to pick up object");
                        })),
                    }
                ),
                new BTSequenceNode(new List<BTNode> // Wander
                {
                    new BTConditionNode(() => 
                        Random.value <= Mathf.Clamp01(brain.emotions["happiness"].Value / 2f + 0.5f)),
                    new BTActionNode(new BlobWanderTargetAction(brain)),
                    new BTActionNode(new BlobGoToTargetAction(brain)),
                }),
                new BTActionNode(new BlobIdleAction(brain)), // Idle
            });
            
        }
    }
}