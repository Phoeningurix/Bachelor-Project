using System.Collections.Generic;
using AgentLogic.AgentActions.BlobActions;
using UnityEngine;

namespace AgentLogic.BehaviorTree
{
    public class SimpleBlobBehaviorTree : BehaviorTree
    {
        

        public SimpleBlobBehaviorTree(BlobBrain brain)
        {
            Root = new BTSelectorNode(new List<BTNode>
            {
                new BTSequenceNode(new List<BTNode>
                {
                    new BTConditionNode(() => Random.value <= Mathf.Clamp01(brain.emotions["happiness"].Value / 2f + 0.5f)),
                    new BTActionNode(new BlobWanderTargetAction(brain)),
                    new BTActionNode(new BlobWanderAction(brain)),
                }),
                new BTActionNode(new BlobIdleAction(brain)),
            });

            /*Root = new BTSequenceNode(new List<BTNode>
            {
                new BTSequenceNode(new List<BTNode>
                {
                    new BTActionNode(new BlobWanderTargetAction(brain)),
                    new BTActionNode(new BlobWanderAction(brain)),
                }).Repeat(2),
                new BTActionNode(new BlobIdleAction(brain))
            });*/
        }
    }
}