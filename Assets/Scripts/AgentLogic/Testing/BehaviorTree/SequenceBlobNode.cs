using System.Collections.Generic;

namespace AgentLogic.Testing.BehaviorTree
{
    public class Sequence : BlobNode
    {
        private List<BlobNode> children;

        public Sequence(List<BlobNode> children)
        {
            this.children = children;
        }

        public override NodeState Tick()
        {
            foreach (var child in children)
            {
                var result = child.Tick();
                if (result != NodeState.Success)
                    return result;
            }
            return NodeState.Success;
        }
    }
}