namespace AgentLogic.Testing
{
    /*public class BlobIdleAction: AgentAction<BlobBrain>
    {
        private readonly float _waitTime;
        private float _timeSinceStart;

        public BlobIdleAction(float waitTime)
        {
            _waitTime = waitTime;
        }

        public override void OnStart(BlobBrain agent)
        {
            _timeSinceStart = 0f;
            
        }

        public override void OnStop(BlobBrain agent)
        {
            // Don't do anything
        }

        public override bool Tick(BlobBrain agent, float deltaTime)
        {
            _timeSinceStart += deltaTime;
            
            return _timeSinceStart >= _waitTime;
        }
    }*/
}