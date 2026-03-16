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
                                     + brain.personalityTraits["extraversion"].Value * 0.7f
                                - brain.emotions["fear"].Value * 0.6f;
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
            float probability = 0.3f + brain.emotions["happiness"].Value * 0.1f
                                + brain.personalityTraits["openness"].Value * 0.7f;
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
            // muss sehr niedrig sein, da sie in jedem Tick getestet wird
            float probability = 0.05f - brain.personalityTraits["conscientiousness"].Value * 0.15f;  // 0 - 0.3
            probability *= brain.DeltaTime(); // Wahrscheinlichkeit pro Sekunde
            return CheckProbability(probability);
        }
        
        //Interaction Weights
        //TODO Choose weights
        public static float GetGreetingWeight(this BlobBrain brain)
        {
            return 0.5f + brain.emotions.GetBetween01("happiness") * 0.2f 
                  + brain.personalityTraits.GetBetween01("agreeableness") * 0.5f 
                  - brain.emotions.GetBetween01("anger") * 0.2f;
        }
        
        public static float GetComplimentWeight(this BlobBrain brain)
        {
            return 0.2f + brain.emotions.GetBetween01("happiness") * 0.2f 
                   + brain.personalityTraits.GetBetween01("extraversion") * 0.2f 
                   + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f
                   - brain.emotions.GetBetween01("fear") * 0.1f
                   - brain.emotions.GetBetween01("anger") * 0.2f;
        }
        
        public static float GetGiftWeight(this BlobBrain brain)
        {
            return 0.3f + brain.emotions.GetBetween01("happiness") * 0.4f 
                   + brain.personalityTraits.GetBetween01("agreeableness") * 0.7f
                   - brain.emotions.GetBetween01("anger") * 0.3f;
        }
        
        public static float GetInsultWeight(this BlobBrain brain)
        {
            return 0.5f + brain.emotions.GetBetween01("anger") * 0.3f 
                   - brain.personalityTraits.GetBetween01("agreeableness") * 0.7f
                   - brain.emotions.GetBetween01("happiness") * 0.1f;
            
        }
        
        public static float GetScreamWeight(this BlobBrain brain)
        {
            return 0.2f + brain.emotions.GetBetween01("anger") * 0.5f
                   - brain.personalityTraits.GetBetween01("agreeableness") * 0.6f
                   + brain.personalityTraits.GetBetween01("extraversion") * 0.2f
                   + brain.emotions.GetBetween01("fear") * 0.7f;
        }
        
    }
}