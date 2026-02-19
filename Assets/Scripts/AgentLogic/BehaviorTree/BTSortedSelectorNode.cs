using System.Collections.Generic;
using System.Linq;
using Utils;

namespace AgentLogic.BehaviorTree
{
    public abstract class BTSortedSelectorNode : BTSelectorNode
    {
        // Normal selector node - but on first trigger it sorts the children first
        
        private bool _hasSorted = false;
        
        public BTSortedSelectorNode(List<BTNode> children) : base(children)
        {
        }
        
        public override NodeState Tick()
        {
            if (!_hasSorted)
            {
                SortChildren(_children);
                _hasSorted = true;
            }
        
            return base.Tick();
        }
        
        protected override void Reset()
        {
            base.Reset();
            _hasSorted = false;
        }

        // This method has to sort in place!!!
        protected abstract void SortChildren(List<BTNode> children);
    }
}