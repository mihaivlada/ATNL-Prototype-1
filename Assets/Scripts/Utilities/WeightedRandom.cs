using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Utilities
{
    public static class WeightedRandom
    {
        public static T Pick<T>(IReadOnlyList<T> items, Func<T, float> getWeight)
        {
            return items[PickIndex(items.Count, index => getWeight(items[index]))];
        }

        public static int PickIndex(int count, Func<int, float> getWeight)
        {
            float totalWeight = 0f;

            for (int index = 0; index < count; index++)
            {
                totalWeight += Mathf.Max(0f, getWeight(index));
            }

            float roll = Random.Range(0f, totalWeight);
            int lastWeightedIndex = 0;

            for (int index = 0; index < count; index++)
            {
                float weight = Mathf.Max(0f, getWeight(index));

                if (weight <= 0f)
                {
                    continue;
                }

                if (roll < weight)
                {
                    return index;
                }

                roll -= weight;
                lastWeightedIndex = index;
            }

            return lastWeightedIndex;
        }
    }
}
