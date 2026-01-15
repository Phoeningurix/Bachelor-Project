namespace AgentLogic.AgentActions
{
    public abstract class AgentAction
    {
        
        // Tick returns true if the action is finished
        public abstract bool Tick();
    }
}