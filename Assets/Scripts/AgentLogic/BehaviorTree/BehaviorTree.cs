namespace AgentLogic.BehaviorTree
{
    public abstract class BehaviorTree : IAgentBehavior
    {
        protected BTNode Root;


        public void Tick()
        {
            Root.Tick();
        }
    }
}