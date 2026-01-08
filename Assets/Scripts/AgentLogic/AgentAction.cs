namespace AgentLogic
{
    public abstract class AgentAction<T>
    {
        public abstract void OnStart(T agent);
        
        public abstract void OnStop(T agent);
        
        // Tick returns true if the action is finished
        public abstract bool Tick(T agent, float deltaTime);
    }
}