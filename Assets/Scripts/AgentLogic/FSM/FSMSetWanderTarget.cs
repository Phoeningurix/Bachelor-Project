using AgentLogic.AgentActions.BlobActions;

namespace AgentLogic.FSM
{
    public class FSMSetWanderTarget : IState
    {
        private readonly BlobBrain _brain;
        private readonly BlobWanderTargetAction _wanderTargetAction;
        
        public FSMSetWanderTarget(BlobBrain brain)
        {
            _brain = brain;
            _wanderTargetAction = new BlobWanderTargetAction(brain);
        }
        
        public void Tick()
        {
            _wanderTargetAction.Tick();
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}