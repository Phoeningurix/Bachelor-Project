using UnityEngine;
using System.Collections.Generic;

namespace AgentLogic.Testing.BehaviorTree
{
    public class SelectorBlobNode : BlobNode
    {
        private List<BlobNode> _children;

        public SelectorBlobNode(List<BlobNode> children)
        {
            _children = children;
        }

        public override NodeState Tick()
        {
            foreach (var child in _children)
            {
                var result = child.Tick();
                if (result != NodeState.Failure)
                    return result;
            }
            return NodeState.Failure;
        }
    }
}