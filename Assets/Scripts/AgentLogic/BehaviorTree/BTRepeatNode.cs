using System.Collections.Generic;
using UnityEngine;

namespace AgentLogic.BehaviorTree
{
    public class BTRepeatNode : BTNode
    {
        private readonly int _count;
        private int _currentCount;

        // count = -1 repeats until failure
        public BTRepeatNode(BTNode child, int count = -1) : base(new List<BTNode>{child})
        {
            _count = count;
            _currentCount = 0;
        } 
        
        public override NodeState Tick()
        {
            switch (CurrentChild.Tick())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Failure:
                    _currentCount = 0;
                    break;
                case NodeState.Success:
                    if (_count > -1)
                    {
                        _currentCount++;
                        if (_currentCount >= _count)
                        {
                            _currentCount = 0; // Reset
                            Reset(); // Nicht notwendig
                            return NodeState.Success;
                        }
                    }
                    
                    return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}