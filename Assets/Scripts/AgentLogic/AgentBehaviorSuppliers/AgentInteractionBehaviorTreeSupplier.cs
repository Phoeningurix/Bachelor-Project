using AgentLogic.BehaviorTree;
using UnityEngine;

namespace AgentLogic.AgentBehaviorSuppliers
{
    [CreateAssetMenu(fileName = "AgentInteractionBehaviorTree", menuName = "AgentBehavior/AgentInteractionBehaviorTree")]
    public class AgentInteractionBehaviorTreeSupplier : AgentBehaviorSupplier<BlobBrain>
    {
        public override IAgentBehavior GetAgentBehavior(BlobBrain brain)
        {
            return new AgentInteractionBehaviorTree(brain);
        }
    }
}