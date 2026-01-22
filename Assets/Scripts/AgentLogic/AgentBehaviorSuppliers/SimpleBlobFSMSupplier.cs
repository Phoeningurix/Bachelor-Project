using AgentLogic.FSM;
using UnityEngine;

namespace AgentLogic.AgentBehaviorSuppliers
{
    [CreateAssetMenu(fileName = "SimpleBlobFSM", menuName = "AgentBehavior/SimpleBlobFSM")]
    public class SimpleBlobFSMSupplier : AgentBehaviorSupplier<BlobBrain>
    {
        public override IAgentBehavior GetAgentBehavior(BlobBrain agent)
        {
            return new SimpleBlobFSM(agent);
        }
    }
}