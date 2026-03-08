using UnityEngine;
using UnityEngine.InputSystem;

namespace AgentLogic
{
    public static class DecisionUtils
    {

        public static bool CheckProbability(float probability) 
            => Random.value < Mathf.Clamp01(probability);

        public static bool CheckIgnore(BlobBrain brain)
        {
            //if (Keyboard.current.iKey.isPressed)
            //    return true;
            float probability = 0.8f + brain.emotions["happiness"].Value * 0.1f
                                     + brain.personalityTraits["extraversion"].Value * 0.4f
                                - brain.emotions["fear"].Value * 0.2f;
            return CheckProbability(1f - probability);
        }
        
        public static bool CheckSendInteraction(BlobBrain brain)
        {
            if (Keyboard.current.iKey.isPressed)
                return true;
            // TODO adjust values
            float probability = 0.3f + brain.emotions["happiness"].Value * 0.1f
                                     + brain.personalityTraits["extraversion"].Value * 0.4f
                                - brain.emotions["fear"].Value * 0.2f;
            return CheckProbability(1f - probability);
        }

        public static bool CheckInteractWithObject(BlobBrain brain)
        {
            float probability = 0.3f - brain.emotions["happiness"].Value * 0.5f
                                + brain.personalityTraits["openness"].Value * 0.5f;
            return CheckProbability(probability);
        }

        public static bool CanWander(BlobBrain brain)
        {
            return Random.value <= brain.emotions.GetBetween01("happiness");
        }

        public static float GetSpeed(BlobBrain brain)
        {
            float happiness = brain.emotions.GetBetween01("happiness");
            return brain.Blackboard.Get<float>("wanderSpeed") * Mathf.Lerp(0.5f, 1.5f, happiness);
        }

        public static bool CheckInterruptCurrentAction(BlobBrain brain)
        {
            // has to be low because it is checked every tick
            float probability = 0.05f - brain.personalityTraits["conscientiousness"].Value * 0.15f;  // 0 - 0.3
            probability *= brain.DeltaTime(); // so that the probability is per second
            return CheckProbability(probability);
        }
    }
}