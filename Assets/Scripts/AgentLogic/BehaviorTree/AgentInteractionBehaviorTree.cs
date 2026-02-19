using System.Collections.Generic;
using AgentLogic.AgentActions.BlobActions;
using Interactions;
using Interactions.BlobInteractions;
using UnityEngine;

namespace AgentLogic.BehaviorTree
{
    public class AgentInteractionBehaviorTree : BehaviorTree
    {
        public AgentInteractionBehaviorTree(BlobBrain brain)
        {
            Root = new BTSelectorNode(new List<BTNode>
            { //TODO als erstes prüfen ob es Requests gibt.
                new BTSequenceNode(new List<BTNode>
                {
                    new BTConditionNode(() => brain.InteractionRequests.Count > 0),
                    new BTSelectorNode(new List<BTNode>
                    {
                        new BTSequenceNode(new List<BTNode>
                        {
                            new BTConditionNode(() => Time.time - brain.InteractionRequests[0].TimeStamp 
                                                      > brain.Blackboard.Get<float>("agentInteractionWaitTime")),
                            new BTActionNode(new BlobOneTickAction(brain, b =>
                            {
                                b.InteractionRequests.RemoveAt(0);
                            })),
                        }),
                        new BTSequenceNode(new List<BTNode>
                        {
                            new BTActionNode(new BlobAnswerRequestAction(brain)),
                            new BTActionNode(new BlobOneTickAction(brain, b =>
                            {
                                b.InteractionRequests.RemoveAt(0);
                            })),
                        })
                    }),
                }),
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
                    // new BTActionNode(new BlobAgentInteractionAction(brain)),
                    new BTWeightedRandomSelectorNode(new List<BTWeightedNode>
                    {
                        // pretend these are different actions / different interactions
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Greeting), 
                            () => brain.emotions.GetBetween01("happiness") * 0.2f 
                                + brain.personalityTraits.GetBetween01("extraversion") * 0.2f 
                                - brain.emotions.GetBetween01("fear") * 0.1f),
                        new BTWeightedActionNode(new BlobInteractionRequestAction(
                                brain, 
                                BlobInteractionType.Insult), 
                            () => 0.2f + brain.emotions.GetBetween01("happiness") * 0.3f 
                                  - brain.emotions.GetBetween01("fear") * 0.2f),
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Scream)),  // no weight = 0f always
                        new BTWeightedSequenceNode(
                            () => brain.emotions.GetBetween01("happiness") * 0.2f 
                                  + brain.personalityTraits.GetBetween01("extraversion") * 0.2f 
                                  - brain.emotions.GetBetween01("fear") * 0.1f, new List<BTNode>
                            {
                                new BTConditionNode(() => brain.Blackboard.Get<int>("flowers") > 0),
                                new BTActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Gift))
                            })
                    })
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