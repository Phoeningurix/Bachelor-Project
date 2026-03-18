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
            { 
                new BTSequenceNode(new List<BTNode>
                {
                    new BTConditionNode(() => brain.InteractionRequests.Count > 0),
                    new BTSelectorNode(new List<BTNode>
                    {
                        new BTConditionNode(() => Time.time - brain.InteractionRequests[0].TimeStamp > brain.Blackboard.Get<float>("agentInteractionWaitTime")),
                        new BTSequenceNode(new List<BTNode>
                        {
                            // Test for ignoring of the request
                            new BTConditionNode(() => DecisionUtils.CheckIgnore(brain)),
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
                    new BTConditionNode(() => DecisionUtils.CheckSendInteraction(brain)),
                    new BTWeightedRandomSelectorNode(new List<BTWeightedNode>
                    {
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Greeting), 
                            brain.GetGreetingWeight),
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Insult), 
                            brain.GetInsultWeight),
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Compliment),
                            brain.GetComplimentWeight),
                        new BTWeightedActionNode(new BlobInteractionRequestAction(brain, BlobInteractionType.Scream),
                            brain.GetScreamWeight),
                        new BTWeightedSequenceNode(
                            brain.GetGiftWeight, new List<BTNode>
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
                            //Debug.Log("Picked up object");
                        }, () =>
                        {
                           brain.ModifyEmotion("happiness", -0.1f);
                            //Debug.Log("Failed to pick up object");
                        })),
                    }),
                new BTSequenceNode(new List<BTNode> // Wander
                {
                    new BTConditionNode(() => DecisionUtils.CanWander(brain)),
                    new BTActionNode(new BlobWanderTargetAction(brain)),
                    new BTActionNode(new BlobGoToTargetAction(brain)),
                }),
                new BTActionNode(new BlobIdleAction(brain)), // Idle
            });
            
        }
    }
}