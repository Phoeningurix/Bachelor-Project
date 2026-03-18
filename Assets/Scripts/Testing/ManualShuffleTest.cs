using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Testing
{
    public class ManualShuffleTest : MonoBehaviour 
    {
        public List<float> testWeights = new List<float> { 1f, 10f, 0.5f, 5f };

        [ContextMenu("Run Manual Shuffle")]
        void RunTest() 
        {
            List<int> indices = Enumerable.Range(0, testWeights.Count).ToList();
            ListUtils.WeightedShuffleInPlace(indices, i => testWeights[i]);
        
            string result = "Shuffle Result (Indices): " + string.Join(", ", indices);
            Debug.Log(result);
        }
    }
}