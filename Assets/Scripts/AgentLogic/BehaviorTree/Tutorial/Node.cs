using System.Collections.Generic;

namespace AgentLogic.Testing.BehaviorTree
{

    public class BehaviorTree : Node
    {
        public BehaviorTree(string name) : base(name) { }

        public override Status Process()
        {
            while (currentChildIndex < children.Count)
            {
                var status = children[currentChildIndex].Process();
                if (status != Status.Success)
                {
                    return status;
                }
                currentChildIndex++;
            }
            return Status.Success;
        }
    }

    public class Leaf : Node
    {
        public readonly BlobStrategy.Strategy strategy;

        public Leaf(string name, BlobStrategy.Strategy strategy) : base(name)
        {
            this.strategy = strategy;
        }
        
        public override Status Process() => strategy.Process();
        
        public override void Reset() => strategy.Reset();
        
        
    }
    
    public class Node
    {
        public enum Status { Success, Failure, Running }
        
        public readonly string Name;

        public readonly List<Node> children = new();
        protected int currentChildIndex;

        public Node(string name = "Node")
        {
            this.Name = name;
        }
        
        public void AddChild(Node childNode) => children.Add(childNode);

        public virtual Status Process() => children[currentChildIndex].Process();

        public virtual void Reset()
        {
            currentChildIndex = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
        
        
        

    }
}