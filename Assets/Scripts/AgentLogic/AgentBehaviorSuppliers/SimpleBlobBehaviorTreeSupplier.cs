using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.AgentBehaviorSuppliers
{
    [CreateAssetMenu(fileName = "SimpleBlobBehaviorTree", menuName = "AgentBehavior/SimpleBlobBehaviorTree")]
    public class SimpleBlobBehaviorTreeSupplier : AgentBehaviorSupplier<BlobBrain>
    {
        public override IAgentBehavior GetAgentBehavior(BlobBrain brain)
        {
            return new SimpleBlobBehaviorTree(brain);
        }
    }
}