using System.Collections.Generic;
using Testing;

namespace AgentLogic.Testing.BehaviorTree
{
    public class BlobBehaviorTree 
    {
        // Hier den Baum zusammensetzen
        public BlobNode root;
        
        public float wanderTime = 2f;
        public float waitTime = 0.5f;

        public BlobBehaviorTree(BlobBrain brain)
        {
            root = new SelectorBlobNode(new List<BlobNode>
            {
                new Sequence(new List<BlobNode>
                {
                    new BlobWanderConditionNode(brain),
                    new BlobWanderTargetNode(brain),
                    new BlobWanderNode(brain, wanderTime)
                }),
                new BlobIdleNode(brain, waitTime)
            });
        }
        
    }
}