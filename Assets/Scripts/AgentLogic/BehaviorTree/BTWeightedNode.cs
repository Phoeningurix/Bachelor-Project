using System;
using System.Collections.Generic;

namespace AgentLogic.BehaviorTree
{
    public abstract class BTWeightedNode : BTNode
    {
        private readonly Func<float> _weightGetter;

        public float GetWeight => _weightGetter?.Invoke() ?? 0f;  // if null returns 0f

        public BTWeightedNode(Func<float> weightGetter = null, List<BTNode> children = null) : base(children)
        {
            _weightGetter = weightGetter;
        }
    }
}