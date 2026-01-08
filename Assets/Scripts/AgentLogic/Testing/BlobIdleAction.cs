using Testing;

namespace AgentLogic.Testing
{
    public class BlobIdleAction: AgentAction<BlobBrain>
    {
        private float _waitTime;

        public BlobIdleAction(float waitTime)
        {
            _waitTime = waitTime;
        }

        public override void OnStart(BlobBrain agent)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStop(BlobBrain agent)
        {
            throw new System.NotImplementedException();
        }

        public override bool Tick(BlobBrain agent, float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}