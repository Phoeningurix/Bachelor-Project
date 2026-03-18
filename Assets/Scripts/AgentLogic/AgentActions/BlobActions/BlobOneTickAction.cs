using System;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobOneTickAction : AgentAction
    {
        private readonly BlobBrain _agent;
        private readonly Action<BlobBrain> _action;

        public BlobOneTickAction(BlobBrain agent, Action<BlobBrain> action)
        {
            _agent = agent;
            _action = action;
        }

        public override bool Tick()
        {
            _action?.Invoke(_agent);
            return true;
        }
    }
}