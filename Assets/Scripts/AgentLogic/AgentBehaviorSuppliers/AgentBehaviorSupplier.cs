using UnityEngine;

namespace AgentLogic.AgentBehaviorSuppliers
{
    public abstract class AgentBehaviorSupplier<T> : ScriptableObject
    {
        public abstract IAgentBehavior GetAgentBehavior(T agent);
    }
}