using Testing;
using UnityEngine;

namespace AgentLogic.Testing
{
    [CreateAssetMenu(fileName = "BlobSimpleBehavior", menuName = "AgentBehavior/BlobSimpleBehavior")]
    public class BlobSimpleBehavior: AgentBehavior<BlobBrain>
    {
        public float wanderRadius = 2f;
        public float wanderTime = 1f;
        public float waitTime = 2f;
        
        public override AgentAction<BlobBrain> GetAction(BlobBrain brain)
        {
            float wanderProbability = Mathf.Clamp01(brain.emotions["happiness"].Value / 2f + 0.5f);
            if (Random.value <= wanderProbability)
            {
                return new BlobWanderAction(wanderRadius, wanderTime);
            }
            else
            {
                return new BlobIdleAction(waitTime);
            }

        }
    }
}