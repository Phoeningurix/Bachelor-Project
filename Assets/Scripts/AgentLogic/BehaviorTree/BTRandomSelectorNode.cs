using System.Collections.Generic;
using Utils;

namespace AgentLogic.BehaviorTree
{
    public class BTRandomSelectorNode : BTSortedSelectorNode
    {
        public BTRandomSelectorNode(List<BTNode> children) : base(children)
        {
        }

        protected override void SortChildren(List<BTNode> children)
        {
            ListUtils.ShuffleInPlace(children);
        }
    }
}