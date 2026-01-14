using System.Collections.Generic;

namespace AgentLogic.Testing.BehaviorTree
{
    public abstract class BlobNode
    {
        
        public enum NodeState
        {
            Success,
            Failure,
            Running
        }
        
        public abstract NodeState Tick();
        
        
        
    }
}