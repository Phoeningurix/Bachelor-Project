namespace AgentLogic.BehaviorTree
{
    public static class BTNodeExtensions
    {
        public static BTNode Invert(this BTNode node) => new BTInvertNode(node);
        
        public static BTNode Repeat(this BTNode node, int count = -1) => new BTRepeatNode(node, count);
        
    }
}