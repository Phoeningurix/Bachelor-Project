using System;
using System.Collections.Generic;

namespace Utils
{
    public class ListUtils
    {
        public static void ShuffleInPlace<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);

                (list[k], list[n]) = (list[n], list[k]);  // swap
            }
        }
        
        public static void WeightedShuffleInPlace<T>(List<T> list, Func<T, float> weightSelector)
        {
            int count = list.Count;
            if (count <= 1) return;

            // 1. Create a temporary list binding items to their randomized scores
            var scoredItems = new List<(T Item, float Score)>(count);

            for (int i = 0; i < count; i++)
            {
                T item = list[i];
                float weight = weightSelector(item);

                // Weights must be greater than 0 for this algorithm. 
                if (weight <= 0) weight = 0.00001f;

                float u = UnityEngine.Random.value;
                // Prevent log(0)
                if (u <= 0) u = 0.00001f; 

                // 2. Stable calculation of U^(1/W) logic
                // We use log(U) / W. Because log(U) is negative, higher weights 
                // pull the score closer to 0 (which is mathematically higher).
                float score = (float)(Math.Log(u) / weight);

                scoredItems.Add((item, score));
            }

            // 3. Sort the paired list based on the generated scores (Descending)
            scoredItems.Sort((a, b) => b.Score.CompareTo(a.Score));

            // 4. Write back the shuffled items to the original list
            for (int i = 0; i < count; i++)
            {
                list[i] = scoredItems[i].Item;
            }
        }
        
    }
}