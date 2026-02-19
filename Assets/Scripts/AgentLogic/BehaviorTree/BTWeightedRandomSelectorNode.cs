using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AgentLogic.BehaviorTree
{
    public class BTWeightedRandomSelectorNode : BTSortedSelectorNode
    {
        public BTWeightedRandomSelectorNode(List<BTWeightedNode> children) : base(children.Cast<BTNode>().ToList())
        {
        }

        protected override void SortChildren(List<BTNode> children)
        {
            ListUtils.WeightedShuffleInPlace(children, node => ((BTWeightedNode)node).GetWeight);
        }
    }
}