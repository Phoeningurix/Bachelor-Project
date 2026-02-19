using Interactions.BlobInteractions;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobGreetingInteractionAction : BlobInteractionRequestAction
    {

        public BlobGreetingInteractionAction(BlobBrain agent) 
            : base(agent, BlobInteractionType.Greeting)
        {
            
        }
    }
}