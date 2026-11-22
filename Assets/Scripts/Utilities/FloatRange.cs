using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Utilities
{
    [Serializable]
    public struct FloatRange
    {
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        [field: SerializeField, Min(0f)] public float Min { get; private set; }
        [field: SerializeField, Min(0f)] public float Max { get; private set; }

        public float GetRandom() => Random.Range(Min, Mathf.Max(Min, Max));
    }
}
