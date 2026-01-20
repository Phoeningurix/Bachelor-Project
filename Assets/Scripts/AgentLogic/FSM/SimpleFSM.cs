namespace AgentLogic.FSM
{
    public class SimpleFSM
    {
        private StateMachine _stateMachine;

        void Awake()
        {
            _stateMachine = new StateMachine();
        }
        
    }
}