using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public abstract class BTNode
    {
        
        public enum NodeState
        {
            Success,
            Failure,
            Running
        }

        protected readonly List<BTNode> _children;
        
        protected int CurrentChildIndex = 0;

        protected virtual void Reset() => CurrentChildIndex = 0;

        public BTNode CurrentChild => _children[CurrentChildIndex];
        
        public int ChildrenCount => _children.Count;

        protected BTNode(List<BTNode> children = null)
        {
            _children = children;
        }
        
        public abstract NodeState Tick();
        
        
        
    }
}