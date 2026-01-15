namespace AgentLogic.BehaviorTree
{
    public class SimpleBlobBehaviorTree : BehaviorTree
    {
        
        public float WanderTime = 2f;
        public float WaitTime = 0.5f;

        public SimpleBlobBehaviorTree(BlobBrain brain)
        {
            Root = new BTConditionNode(() => brain.emotions["happyness"].Value > 0f);



            /*root = new SelectorBlobNode(new List<BlobNode>
            {
                new Sequence(new List<BlobNode>
                {
                    new BlobWanderConditionNode(brain),
                    new BlobWanderTargetNode(brain),
                    new BlobWanderNode(brain, wanderTime)
                }),
                new BlobIdleNode(brain, waitTime)
            });*/
        }
    }
}