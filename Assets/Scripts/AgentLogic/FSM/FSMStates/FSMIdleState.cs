
namespace AgentLogic.FSM.FSMStates
{
    public class FSMIdleState : IState
    {
        private readonly BlobBrain _brain;
        private float _idleTimeSinceStart;
        private float _waitTime;
        
        public bool IsIdle() => _idleTimeSinceStart <= _waitTime;

        public FSMIdleState(BlobBrain brain)
        {
            _brain = brain;
        }
        
        public void Tick()
        {
            _idleTimeSinceStart += _brain.DeltaTime();
        }
        

        public void OnEnter()
        {
            _idleTimeSinceStart = 0f;
            _waitTime = _brain.Blackboard.Get<float>("waitTime");
        }

        public void OnExit()
        {
        }
    }
}