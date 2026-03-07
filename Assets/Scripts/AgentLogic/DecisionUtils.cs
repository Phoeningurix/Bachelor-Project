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
            //if (Keyboard.current.iKey.isPressed)
            //    return true;
            // TODO adjust values
            float probability = 0.8f + brain.emotions["happiness"].Value * 0.1f
                                     + brain.personalityTraits["extraversion"].Value * 0.4f
                                - brain.emotions["fear"].Value * 0.2f;
            return CheckProbability(1f - probability);
        }
        
    }
}