using System.Collections.Generic;
using AgentLogic.AgentActions.BlobActions;
using Interactions;
using Interactions.BlobInteractions;
using NUnit.Framework;
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
                        new BTConditionNode(() => Time.time - brain.InteractionRequests[0].TimeStamp > brain.Blackboard.Get<float>("agentInteractionWaitTime")),
                        new BTSequenceNode(new List<BTNode>
                        {
                            new BTConditionNode(() => // Test for ignoring of the request
                            {
                                float probability = 0.8f + brain.emotions["happiness"].Value * 0.1f
                                                         + brain.personalityTraits["extraversion"].Value * 0.4f
                                                    - brain.emotions["fear"].Value * 0.2f;
                                return Random.value > Mathf.Clamp01(probability);
                            }),
                            // even when ignoring, we need to adjust emotions
                            new BTActionNode(new BlobOnRequestAdjustEmotions(brain)),
                        }),
                        new BTSequenceNode(new List<BTNode>
                        {
                            new BTActionNode(new BlobAnswerRequestAction(brain)),
                            new BTActionNode(new BlobOnRequestAdjustEmotions(brain))
                        })
                    }),
                    new BTActionNode(new BlobOneTickAction(brain, b =>
                    {
                        b.InteractionRequests.RemoveAt(0);
                    })),
                }),
                new BTSequenceNode(new List<BTNode> // Interact with other agents
                {
                    new BTConditionNode(() => 
                        Time.time - brain.Blackboard.Get<float>("lastAgentInteractionCompleted") > 
                        brain.Blackboard.Get<float>("agentInteractionCooldown")),
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
                        //TODO fix Interactions and weights (add compliments)
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
                                .IsNearObjects(brain.Blackboard.Get<float>("objectVisibilityRadius"))),
                        new BTConditionNode(() => DecisionUtils.CheckInteractWithObject(brain)),
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
                            //Debug.Log("Picked up object");
                        }, () =>
                        {
                           brain.ModifyEmotion("happiness", -0.1f);
                            //Debug.Log("Failed to pick up object");
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