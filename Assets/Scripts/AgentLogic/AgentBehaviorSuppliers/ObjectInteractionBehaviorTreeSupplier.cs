using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.AgentBehaviorSuppliers
{
    [CreateAssetMenu(fileName = "ObjectInteractionBehaviorTree", menuName = "AgentBehavior/ObjectInteractionBehaviorTree")]
    public class ObjectInteractionBehaviorTreeSupplier : AgentBehaviorSupplier<BlobBrain>
    {
        public override IAgentBehavior GetAgentBehavior(BlobBrain brain)
        {
            return new ObjectInteractionBehaviorTree(brain);
        }
    }
}